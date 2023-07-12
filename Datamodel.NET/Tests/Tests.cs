﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datamodel;
using System.Numerics;
using DM = Datamodel.Datamodel;

namespace Datamodel_Tests
{
    public class DatamodelTests
    {
        protected FileStream Binary_9_File = System.IO.File.OpenRead(@"Resources\overboss_run.dmx");
        protected FileStream Binary_5_File = System.IO.File.OpenRead(@"Resources\taunt05_b5.dmx");
        protected FileStream Binary_4_File = System.IO.File.OpenRead(@"Resources\binary4.dmx");
        protected FileStream KeyValues2_1_File = System.IO.File.OpenRead(@"Resources\taunt05.dmx");
        protected FileStream Xaml_File = System.IO.File.OpenRead(@"Resources\xaml_dm.xaml");

        static DatamodelTests()
        {
            var binary = new byte[16];
            new Random().NextBytes(binary);
            var quat = Quaternion.Normalize(new Quaternion(1, 2, 3, 4)); // dmxconvert will normalise this if I don't!

            TestValues_V1 = new List<object>(new object[] { "hello_world", 1, 1.5f, true, binary, null, System.Drawing.Color.Blue,
                new Vector2(1,2), new Vector3(1,2,3), new Vector4(1,2,3,4), quat, new Matrix4x4(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16) });

            TestValues_V2 = TestValues_V1.ToList();
            TestValues_V2[5] = TimeSpan.FromMinutes(5) + TimeSpan.FromTicks(TimeSpan.TicksPerMillisecond / 2);

            TestValues_V3 = TestValues_V1.Concat(new object[] { (byte)0xFF, (UInt64)0xFFFFFFFF }).ToList();
        }

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        protected string OutPath { get { return System.IO.Path.Combine(TestContext.TestResultsDirectory, TestContext.TestName); } }
        protected string DmxSavePath { get { return OutPath + ".dmx"; } }
        protected string DmxConvertPath { get { return OutPath + "_convert.dmx"; } }

        protected void Cleanup()
        {
            System.IO.File.Delete(DmxSavePath);
            System.IO.File.Delete(DmxConvertPath);
        }

        protected static DM MakeDatamodel()
        {
            return new DM("model", 1); // using "model" to keep dxmconvert happy
        }

        protected void SaveAndConvert(Datamodel.Datamodel dm, string encoding, int version)
        {
            dm.Save(DmxSavePath, encoding, version);

            var dmxconvert = new System.Diagnostics.Process();
            dmxconvert.StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = System.IO.Path.Combine(Properties.Resources.ValveSourceBinaries, "dmxconvert.exe"),
                Arguments = String.Format("-i \"{0}\" -o \"{1}\" -oe {2}", DmxSavePath, DmxConvertPath, encoding),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            Console.WriteLine(String.Join(" ", dmxconvert.StartInfo.FileName, dmxconvert.StartInfo.Arguments));
            Assert.IsTrue(File.Exists(dmxconvert.StartInfo.FileName), String.Format("Could not find dmxconvert at {0}", dmxconvert.StartInfo.FileName));
            
            Console.WriteLine();
            
            dmxconvert.Start();
            var err = dmxconvert.StandardOutput.ReadToEnd();
            err += dmxconvert.StandardError.ReadToEnd();
            dmxconvert.WaitForExit();

            Console.WriteLine(err);

            if (dmxconvert.ExitCode != 0)
                throw new AssertFailedException(err);

        }

        /// <summary>
        /// Perform a parallel loop over all elements and attributes
        /// </summary>
        protected void PrintContents(Datamodel.Datamodel dm)
        {
            System.Threading.Tasks.Parallel.ForEach<Datamodel.Element>(dm.AllElements, e =>
            {
                System.Threading.Tasks.Parallel.ForEach(e, a => { ; });
            });
        }

        protected static List<object> TestValues_V1;
        protected static List<object> TestValues_V2;
        protected static List<object> TestValues_V3;
        protected static Guid RootGuid = Guid.NewGuid();

        protected static List<object> AttributeValuesFor(string encoding_name, int encoding_version)
        {
            if (encoding_name == "keyvalues2")
            {
                return encoding_version >= 4 ? TestValues_V3 : TestValues_V2;
            }
            else if (encoding_name == "binary")
            {
                if (encoding_version >= 9)
                    return TestValues_V3;
                else if (encoding_version >= 3)
                    return TestValues_V2;
                else
                    return TestValues_V1;
            }
            else
                throw new ArgumentException("Unrecognised encoding.");
        }

        protected static void Populate(Datamodel.Datamodel dm, string encoding_name, int encoding_version)
        {
            dm.Root = new Element(dm, "root", RootGuid);

            foreach (var value in AttributeValuesFor(encoding_name, encoding_version))
            {
                if (value == null) continue;
                var name = value.GetType().Name;

                dm.Root[name] = value;
                Assert.AreSame(value, dm.Root[name]);

                name += " array";
                var list = value.GetType().MakeListType().GetConstructor(Type.EmptyTypes).Invoke(null) as IList;
                list.Add(value);
                list.Add(value);
                dm.Root[name] = list;
                Assert.AreSame(list, dm.Root[name]);
            }

            dm.Root["Recursive"] = dm.Root;
            dm.Root["NoName"] = new Element();
            dm.Root["ElemArray"] = new ElementArray(new Element[] { new Element(dm, Guid.NewGuid()), new Element(), dm.Root, new Element(dm, "TestElement") });
            dm.Root["ElementStub"] = new Element(dm, Guid.NewGuid());
        }

        private void CompareVector(DM dm, string name, float[] actual)
        {
            var expected = (IEnumerable<float>)dm.Root[name];

            Assert.AreEqual(actual.Count(), expected.Count());

            foreach (var t in actual.Zip(expected, (a, e) => new Tuple<float, float>(a, e)))
                Assert.AreEqual(t.Item1, t.Item2, 1e-6, name);
        }

        protected void ValidatePopulated(string encoding_name, int encoding_version)
        {
            var dm = DM.Load(DmxConvertPath);
            Assert.AreEqual(RootGuid, dm.Root.ID);
            foreach (var value in AttributeValuesFor(encoding_name, encoding_version))
            {
                if (value == null) continue;
                var name = value.GetType().Name;

                if (value is ICollection)
                    CollectionAssert.AreEqual((ICollection)value, (ICollection)dm.Root[name]);
                else if (value is System.Drawing.Color)
                    Assert.AreEqual(((System.Drawing.Color)value).ToArgb(), dm.Root.Get<System.Drawing.Color>(name).ToArgb());
                else if (value is Quaternion)
                {
                    var quat = (Quaternion)value;
                    var expected = dm.Root.Get<Quaternion>(name);
                    Assert.AreEqual(quat.W, expected.W, 1e-6, name + " W");
                    Assert.AreEqual(quat.X, expected.X, 1e-6, name + " X");
                    Assert.AreEqual(quat.Y, expected.Y, 1e-6, name + " Y");
                    Assert.AreEqual(quat.Z, expected.Z, 1e-6, name + " Z");
                }
                else
                    Assert.AreEqual(value, dm.Root[name], name);
            }

            dm.Dispose();
        }

        protected DM Create(string encoding, int version, bool memory_save = false)
        {
            var dm = MakeDatamodel();
            Populate(dm, encoding, version);

            dm.Root["Arr"] = new System.Collections.ObjectModel.ObservableCollection<int>();
            dm.Root.GetArray<int>("Arr");

            if (memory_save)
                dm.Save(new MemoryStream(), encoding, version);
            else
            {
                dm.Save(DmxSavePath, encoding, version);
                SaveAndConvert(dm, encoding, version);
                ValidatePopulated(encoding, version);
                Cleanup();
            }

            dm.AllElements.Remove(dm.Root.GetArray<Element>("ElemArray")[3], DM.ElementList.RemoveMode.MakeStubs);
            Assert.AreEqual<bool>(true, dm.Root.GetArray<Element>("ElemArray")[3].Stub);

            dm.AllElements.Remove(dm.Root, DM.ElementList.RemoveMode.MakeStubs);
            Assert.AreEqual<bool>(true, dm.Root.Stub);

            return dm;
        }
    }

    [TestClass]
    public class Functionality : DatamodelTests
    {

        [TestMethod]
        public void Create_Binary_9()
        {
            Create("binary", 9);
        }
        [TestMethod]
        public void Create_Binary_5()
        {
            Create("binary", 5);
        }
        [TestMethod]
        public void Create_Binary_4()
        {
            Create("binary", 4);
        }
        [TestMethod]
        public void Create_Binary_3()
        {
            Create("binary", 3);
        }
        [TestMethod]
        public void Create_Binary_2()
        {
            Create("binary", 2);
        }

        [TestMethod]
        public void Create_KeyValues2_4()
        {
            Create("keyvalues2", 4);
        }


        [TestMethod]
        public void Create_KeyValues2_1()
        {
            Create("keyvalues2", 1);
        }

        void Get_TF2(Datamodel.Datamodel dm)
        {
            dm.Root.Get<Element>("skeleton").GetArray<Element>("children")[0].Any();
            dm.FormatVersion = 22; // otherwise recent versions of dmxconvert fail
        }

        [TestMethod]
        public void Dota2_Binary_9()
        {
            var dm = DM.Load(Binary_9_File);
            PrintContents(dm);
            dm.Root.Get<Element>("skeleton").GetArray<Element>("children")[0].Any();
            SaveAndConvert(dm, "binary", 9);

            Cleanup();
        }

        [TestMethod]
        public void TF2_Binary_5()
        {
            var dm = DM.Load(Binary_5_File);
            PrintContents(dm);
            Get_TF2(dm);
            SaveAndConvert(dm, "binary", 5);

            Cleanup();
        }

        [TestMethod]
        public void TF2_Binary_4()
        {
            var dm = DM.Load(Binary_4_File);
            PrintContents(dm);
            Get_TF2(dm);
            SaveAndConvert(dm, "binary", 4);

            Cleanup();
        }

        [TestMethod]
        public void TF2_KeyValues2_1()
        {
            var dm = DM.Load(KeyValues2_1_File);
            PrintContents(dm);
            Get_TF2(dm);
            SaveAndConvert(dm, "keyvalues2", 1);

            Cleanup();
        }

        [TestMethod]
        public void Import()
        {
            var dm = MakeDatamodel();
            Populate(dm, "binary", 9);

            var dm2 = MakeDatamodel();
            dm2.Root = dm2.ImportElement(dm.Root, DM.ImportRecursionMode.Recursive, DM.ImportOverwriteMode.All);

            SaveAndConvert(dm, "keyvalues2", 4);
            SaveAndConvert(dm, "binary", 9);
        }

        [TestMethod]
        public void LoadXaml()
        {
            var dm = System.Windows.Markup.XamlReader.Load(Xaml_File) as DM;
            Assert.AreSame(dm.Root.GetArray<Element>("NonStubArray")[0].GetArray<Element>("StubArray")[0], dm.Root.Get<Element>("NonStub"));
            Assert.AreEqual(dm.Root.Get<Element>("NonStub").Get<Vector2>("Vec2"), new Vector2(1, 1));
        }
    }

    [TestClass]
    public class Performance : DatamodelTests
    {
        const int Load_Iterations = 10;
        System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch();

        void Load(FileStream f)
        {
            long elapsed = 0;
            Timer.Start();
            foreach (var i in Enumerable.Range(0, Load_Iterations + 1))
            {
                DM.Load(f, Datamodel.Codecs.DeferredMode.Disabled);
                if (i > 0)
                {
                    Console.WriteLine(Timer.ElapsedMilliseconds);
                    elapsed += Timer.ElapsedMilliseconds;
                }
                Timer.Restart();
            }
            Timer.Stop();
            Console.WriteLine("Average: {0}ms", elapsed / Load_Iterations);
        }
        [TestMethod]
        public void Perf_Load_Binary5()
        {
            Load(Binary_5_File);
        }

        [TestMethod]
        public void Perf_Load_KeyValues2_1()
        {
            Load(KeyValues2_1_File);
        }

        [TestMethod]
        public void Perf_Create_Binary5()
        {
            foreach (var i in Enumerable.Range(0, 1000))
                Create("binary", 5, true);
        }

        [TestMethod]
        public void Perf_CreateElements_Binary5()
        {
            var dm = MakeDatamodel();
            dm.Root = new Element(dm, "root");
            var inner_elem = new Element(dm, "inner_elem");
            var arr = new ElementArray(20000);
            dm.Root["big_array"] = arr;

            foreach (int i in Enumerable.Range(0, 19999))
                arr.Add(inner_elem);

            SaveAndConvert(dm, "binary", 5);
            Cleanup();
        }

        [TestMethod]
        public void Perf_CreateAttributes_Binary5()
        {
            var dm = MakeDatamodel();
            dm.Root = new Element(dm, "root");

            foreach (int x in Enumerable.Range(0, 5000))
            {
                var elem_name = x.ToString();
                foreach (int i in Enumerable.Range(0, 5))
                {
                    var elem = new Element(dm, elem_name);
                    var key = i.ToString();
                    elem[key] = i;
                    elem.Get<int>(key);
                }
            }

            SaveAndConvert(dm, "binary", 5);
            Cleanup();
        }
    }

    static class Extensions
    {
        public static Type MakeListType(this Type t)
        {
            return typeof(System.Collections.Generic.List<>).MakeGenericType(t);
        }
    }
}
