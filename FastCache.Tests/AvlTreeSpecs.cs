using System;
using System.Text;
using FastCache.Core.Providers;
using Xunit;

namespace FastCache.Tests
{
    public class AvlTreeSpecs
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 10 })]
        public void Insert_WhenAllAreOnRight_ShouldBalance_WithHeightAs1(int[] expectedOutput)
        {
            ReadOnlySpan<int> s = expectedOutput.AsSpan();

            var treeProvider = new TestAvlTreeProvider();

            treeProvider.Insert("1", 1);
            treeProvider.Insert("2", 2);
            treeProvider.Insert("10", 10);

            var spanIdx = 0;
            foreach (var node in treeProvider)
            {
                Assert.Equal(s[spanIdx++], node);
            }
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 10 })]
        public void Insert_WhenAllAreOnLeft_ShouldBalance_WithHeightAs1(int[] expectedOutput)
        {
            ReadOnlySpan<int> s = expectedOutput.AsSpan();

            var treeProvider = new TestAvlTreeProvider();
            
            treeProvider.Insert("10", 10);
            treeProvider.Insert("2", 2);
            treeProvider.Insert("1", 1);

            var spanIdx = 0;
            foreach (var node in treeProvider)
            {
                Assert.Equal(s[spanIdx++], node);
            }
        }

        [Theory]
        [InlineData(new int[] {1, 2, 7, 8, 10, 20})]
        public void Insert_WhenThereIsLeftRightCase_ShouldHaveBalanceFactorAs1(int[] expectedOutput)
        {
            ReadOnlySpan<int> s = expectedOutput.AsSpan();

            var treeProvider = new TestAvlTreeProvider();

            treeProvider.Insert("10", 10);
            treeProvider.Insert("2", 2);
            treeProvider.Insert("20", 20);
            treeProvider.Insert("1", 1);
            treeProvider.Insert("8", 8);
            treeProvider.Insert("7", 7);

            var spanIdx = 0;
            foreach (var node in treeProvider)
            {
                Assert.Equal(s[spanIdx++], node);
            }
        }

        [Fact]
        public void Get_WhenElementExists_ShouldReturnValue()
        {
            var treeProvider = new TestAvlTreeProvider();

            treeProvider.Insert("10", 10);
            treeProvider.Insert("2", 2);
            treeProvider.Insert("20", 20);
            treeProvider.Insert("1", 1);
            treeProvider.Insert("8", 8);
            treeProvider.Insert("7", 7);

            var result = treeProvider.Get("7");

            Assert.Equal(7, result);
        }
    }

    public sealed class TestAvlTreeProvider : AvlTreeInMemoryProvider
    {
        protected override int GetHashCode(string key) => int.Parse(key);
    }
}
