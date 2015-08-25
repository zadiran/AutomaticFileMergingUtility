using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Code.Logic;
using Types;

namespace DefaultImplementationTestsProject
{
    [TestClass]
    public class StringAnalyserTests
    {    
        [TestMethod]
        public void FullCoincidence()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();
            
            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 100;
            expected.IsEqual = true;
            
            var actual = analyser.CompareStrings("12345", "12345");
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FullMismatch()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 0;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "abcde");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletionAtTheEnd()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 78;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "1234");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletionAtTheStart()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 78;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "2345");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletionInCenter()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 78;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "1345");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyDeletionsInCenter()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 75;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("123456789", "1345689");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyDeletionsInCenterAndRepeatingSubstrings()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 56;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("1234512345", "134245");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AdditionAtTheEnd()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 98;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "123456");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AdditionAtTheStart()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 98;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "012345");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AdditionInCenter()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 98;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12456", "123456");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyAdditionsInCenter()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 94;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "1a23a4a5");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyAdditionsAndDeletions1()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 50;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345", "a2a4a5");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyAdditionsAndDeletions2()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 48;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("1234567890", "fd34aaf6f78ar0");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ManyAdditionsAndDeletions3()
        {
            var analyser = new DefaultStringAnalyser();
            analyser.ResultPrototype = new DefaultStringAnalysisResult();

            var expected = new DefaultStringAnalysisResult();
            expected.Equality = 42;
            expected.IsEqual = false;

            var actual = analyser.CompareStrings("12345678901234567890", "126s7r80ygf1h2hseff3sdg78ddsd");

            Assert.AreEqual(expected, actual);
        }
    }
}
