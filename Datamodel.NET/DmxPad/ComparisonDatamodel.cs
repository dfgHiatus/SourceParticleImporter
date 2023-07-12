﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Datamodel;
using AttrKVP = System.Collections.Generic.KeyValuePair<string, object>;

namespace DmxPad
{
    public class ComparisonDatamodel
    {
        #region Properties

        public Datamodel.Datamodel Datamodel_Left { get; private set; }
        public Datamodel.Datamodel Datamodel_Right { get; private set; }
        public Dictionary<Guid, Element> ComparedElements { get; private set; }
        public Element Root { get; protected set; }

        #endregion

        public ComparisonDatamodel(Datamodel.Datamodel dm_left, Datamodel.Datamodel dm_right)
        {
            Datamodel_Left = dm_left;
            Datamodel_Right = dm_right;
            ComparedElements = new Dictionary<Guid, Element>();

            Root = new ComparisonDatamodel.Element(this, Datamodel_Left.Root, Datamodel_Right.Root);
        }

        public enum ComparisonState
        {
            Unchanged,
            ChildChanged,
            Changed,
            Added,
            Removed,
        }

        public interface IComparisonItem
        {
            ComparisonState State { get; }
        }

        public class Element : IComparisonItem, IEnumerable<Attribute>
        {
            #region Properties
            public ComparisonDatamodel Owner { get; protected set; }

            public ComparisonState State
            {
                get
                {
                    var state = _State;
                    if (state == ComparisonState.Unchanged && this.Any(a => a.State != ComparisonState.Unchanged))
                        state = ComparisonState.ChildChanged;
                    return state;
                }
                protected set { _State = value; }
            }
            ComparisonState _State = ComparisonState.Unchanged;

            public Datamodel.Element Element_Left { get; protected set; }
            public Datamodel.Element Element_Right { get; protected set; }

            OrderedDictionary Attributes = new OrderedDictionary();

            public string ClassName
            {
                get { return Element_Right != null ? Element_Right.ClassName : Element_Left.ClassName; }
            }
            #endregion

            public static readonly object NoAttributeValue = new object();

            public Element(ComparisonDatamodel owner, Datamodel.Element elem_left, Datamodel.Element elem_right)
            {
                Owner = owner;
                Element_Left = elem_left;
                Element_Right = elem_right;

                if (Element_Left == null)
                    State = ComparisonState.Added;
                else if (Element_Right == null)
                    State = ComparisonState.Removed;
                else if (Element_Left.Name != Element_Right.Name ||
                    Element_Left.ID != Element_Right.ID ||
                    Element_Left.ClassName != Element_Right.ClassName ||
                    Element_Left.Stub != Element_Right.Stub
                    || !Enumerable.SequenceEqual(Element_Left.Select(a => a.Key), Element_Right.Select(a => a.Key)))
                    State = ComparisonState.Changed;

                if (Element_Right != null)
                {
                    Owner.ComparedElements[Element_Right.ID] = this;
                    foreach (var attr_right in Element_Right.Where(a => Element_Left != null && !Element_Left.ContainsKey(a.Key)))
                        Attributes.Add(attr_right.Key, new Attribute(this,attr_right.Key, NoAttributeValue, attr_right.Value));
                }

                if (Element_Left != null)
                {
                    Owner.ComparedElements[Element_Left.ID] = this;
                    foreach (var attr_left in Element_Left)
                    {
                        object value_right = NoAttributeValue;
                        if (Element_Right != null && Element_Right.ContainsKey(attr_left.Key))
                            value_right = Element_Right[attr_left.Key];
                        Attributes.Add(attr_left.Key, new Attribute(this, attr_left.Key, attr_left.Value, value_right));
                    }
                }
            }

            public IEnumerator<Attribute> GetEnumerator()
            {
                foreach (var attr in Attributes)
                {
                    var entry = (DictionaryEntry)attr;
                    yield return (Attribute)entry.Value;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
        }

        public class Attribute : IComparisonItem, IEnumerable<IComparisonItem>
        {
            #region Properties
            public Element Owner
            {
                get { return _Owner; }
                protected set { _Owner = value; }
            }
            Element _Owner;

            public string Name
            {
                get { return _Name; }
                protected set { _Name = value; }
            }
            string _Name;

            public ComparisonState State
            {
                get
                {
                    var state = _State;
                    if (state == ComparisonState.Unchanged)
                    {
                        var elem = Value_Combined as Element;
                        if (elem != null)
                        {
                            if (elem.State != ComparisonState.Unchanged)
                                state = ComparisonState.ChildChanged;
                        }
                        else
                        {
                            var array = Value_Combined as IList<Element>;
                            if (array != null && array.Any(e => e.State != ComparisonState.Unchanged))
                                state = ComparisonState.ChildChanged;
                        }
                    }
                    return state;
                }
                protected set { _State = value; }
            }
            ComparisonState _State = ComparisonState.Unchanged;

            public object Value_Left { get; set; }
            public object Value_Right { get; set; }

            public object Value_Combined
            {
                get { return _Value_Combined; }
                protected set { _Value_Combined = value; }
            }
            object _Value_Combined;

            #endregion

            public Attribute(Element owner, string name, object value_left, object value_right)
            {
                Owner = owner;
                Value_Left = value_left;
                Value_Right = value_right;

                var cdm = Owner.Owner;

                Name = name;

                if (value_left == Element.NoAttributeValue)
                {
                    State = ComparisonState.Added;
                    Value_Combined = value_right;
                }
                else if (value_right == Element.NoAttributeValue)
                {
                    State = ComparisonState.Removed;
                    Value_Combined = value_left;
                }
                else
                {
                    if (!Datamodel.ValueComparer.Default.Equals(value_left, value_right))
                        State = ComparisonState.Changed;

                    if (Value_Left == null)
                        Value_Combined = Value_Right;
                    else if (Value_Right == null)
                        Value_Combined = Value_Left;
                    else
                    {
                        if (Value_Left.GetType() == typeof(Datamodel.Element))
                        {
                            var compare_elem = new Element(cdm, (Datamodel.Element)Value_Left, (Datamodel.Element)Value_Right);
                            Value_Combined  = compare_elem;
                            State = compare_elem.State;
                        }
                        else
                        {
                            var inner = Datamodel.Datamodel.GetArrayInnerType(Value_Left.GetType());
                            if (inner == typeof(Datamodel.Element))
                            {
                                var combined_array = ((IList<Datamodel.Element>)Value_Left)
                                    .Concat((IList<Datamodel.Element>)Value_Right)
                                    .Distinct(Datamodel.Element.IDComparer.Default)
                                    .Select(e => new Element(cdm, cdm.Datamodel_Left.AllElements[e.ID], cdm.Datamodel_Right.AllElements[e.ID])).ToArray();
                                Value_Combined = combined_array;

                                foreach (var elem_ in combined_array)
                                {
                                    switch (elem_.State)
                                    {
                                        case ComparisonState.ChildChanged:
                                        case ComparisonState.Changed:
                                            if (State < ComparisonState.ChildChanged)
                                                State = ComparisonState.ChildChanged;
                                            break;
                                        case ComparisonState.Added:
                                        case ComparisonState.Removed:
                                            if (State < ComparisonState.Changed)
                                                State = ComparisonState.Changed;
                                            break;
                                    }
                                }
                            }
                            else
                                Value_Combined = Value_Right;
                        }
                    }
                }
            }

            public IEnumerator<IComparisonItem> GetEnumerator()
            {
                var elem = Value_Combined as Element;
                if (elem != null)
                {
                    foreach (var attr in elem)
                        yield return attr;
                    yield break;
                }

                var elem_arr = Value_Combined as IEnumerable<Element>;
                if (elem_arr != null)
                {
                    foreach (var elem_ in elem_arr)
                        yield return elem_;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
