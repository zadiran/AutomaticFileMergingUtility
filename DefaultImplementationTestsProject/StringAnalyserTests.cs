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
    }
}
