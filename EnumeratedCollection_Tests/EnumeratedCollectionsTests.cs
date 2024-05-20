using NUnit.Framework;
using System;
using AP.Collections;
using AP.Utilities;

namespace AP.Tests
{
    public enum NonContiguousEnum
    {
        NegativeOne = -1,
        One = 1
    }

    public enum ContiguousEnum
    {
        NegativeOne = -1,
        Zero = 0,
        One = 1
    }

    [TestFixture]
    public class EnumeratredCollectionTests
    {
        [Test]
        public void EnumeratedCollection_Constructor()
        {

            Assert.Throws<InvalidOperationException>(() => new EnumeratedCollection<NonContiguousEnum, int>(), "InvalidOperationException should be thrown when enum parameter is not contiguous");

            var collection = new EnumeratedCollection<ContiguousEnum, int>()
            {
                { ContiguousEnum.NegativeOne, -1 },
                { ContiguousEnum.Zero, 0 },
                { ContiguousEnum.One, 1 }
            };

            Assert.AreEqual(collection.Count, 3, "The Collections initial size should be the size of enum space.");
            Assert.AreEqual(collection[ContiguousEnum.NegativeOne], -1, "The collections should initialze starting values correctly");
            Assert.AreEqual(collection[ContiguousEnum.Zero], 0, "The collections should initialze starting values correctly");
            Assert.AreEqual(collection[ContiguousEnum.One], 1, "The collections should initialze starting values correctly");
            Assert.AreEqual(ContiguousEnum.NegativeOne.AsCollectionIndex(), 0, "The collections should initialze key indices correctly");
            Assert.AreEqual(ContiguousEnum.Zero.AsCollectionIndex(), 1, "The collections should initialze key indices correctly");
            Assert.AreEqual(ContiguousEnum.One.AsCollectionIndex(), 2, "The collections should initialze key indices correctly");

            var intialValueCollection = new EnumeratedCollection<ContiguousEnum, int>(-1);

            for (var i = 0; i < intialValueCollection.Count; i++)
            {
                Assert.AreEqual(intialValueCollection[i], -1, "The collections should initialze starting values correctly");
            }

        }

        [Test]
        public void EnumeratedCollection_Indexer()
        {
            var collection = new EnumeratedCollection<ContiguousEnum, int>();
            collection[ContiguousEnum.One] = 1;
            Assert.AreEqual(1, collection[ContiguousEnum.One]);
            collection[2] = 2;
            Assert.AreEqual(2, collection[ContiguousEnum.One]);
        }

        [Test]
        public void EnumeratedCollection_MakeReadonly()
        {
            var collection = new EnumeratedCollection<ContiguousEnum, int>()
            {
                {ContiguousEnum.One, 1 }
            }.MakeReadonly();

            Assert.Throws<InvalidOperationException>(() => collection[ContiguousEnum.One] = 2);
        }

        [Test]
        public void EnumeratedCollection_IEnumerable()
        {
            // Test the IEnumerable implementation
            var collection = new EnumeratedCollection<ContiguousEnum, int>()
            {
                { ContiguousEnum.NegativeOne, -1 },
                { ContiguousEnum.Zero, 0 },
                { ContiguousEnum.One, 1 }
            };

            var iterator = collection.GetEnumerator();
            // Test that the iterator iterates through collection in the correct order.

            for (var i = 0; i < collection.Count; i++)
            {
                Assert.True(iterator.MoveNext());
                Assert.AreEqual(collection[i], iterator.Current);
            }
        }

        [Test]
        public void EnumeratedCollection_IndexOf()
        {
            var collection = new EnumeratedCollection<ContiguousEnum, int>()
            {
                { ContiguousEnum.NegativeOne, -1 },
                { ContiguousEnum.Zero, 0 },
                { ContiguousEnum.One, 1 }
            };

            Assert.AreEqual(0, collection.IndexOf(-1));
            Assert.AreEqual(1, collection.IndexOf(0));
            Assert.AreEqual(2, collection.IndexOf(1));
            Assert.AreEqual(-1, collection.IndexOf(2));
        }

        [Test]
        public void EnumeratedCollection_TryGetValueOf()
        {
            var collection = new EnumeratedCollection<ContiguousEnum, int>()
            {
                { ContiguousEnum.NegativeOne, -1 },
                { ContiguousEnum.Zero, 0 },
                { ContiguousEnum.One, 1 }
            };

            Assert.True(collection.TryGetKeyOf(0, out var key));
            Assert.AreEqual(ContiguousEnum.Zero, key);
            Assert.False(collection.TryGetKeyOf(2, out key));
        }
    }

    [TestFixture]
    public class EnumUtilityTests
    {
        [Test]
        public void EnumUtility_ValidateIsContiguous()
        {
            Assert.True(EnumUtility<ContiguousEnum>.ValidateIsContiguous());
            Assert.False(EnumUtility<NonContiguousEnum>.ValidateIsContiguous());
        }

        [Test]
        public void EnumUtility_InRange()
        {
            var range = EnumUtility<ContiguousEnum>.InRange(ContiguousEnum.Zero, ContiguousEnum.One);
            ContiguousEnum[] expected = { ContiguousEnum.Zero, ContiguousEnum.One };
            var i = 0;
            foreach (var item in range)
            {
                Assert.AreEqual(expected[i], item);
                i++;
            }

            Assert.AreEqual(i, 2);
        }

        [Test]
        public void EnumUtility_IsInRange()
        {
            Assert.True(ContiguousEnum.Zero.IsInRange(ContiguousEnum.Zero, ContiguousEnum.One));
            Assert.False(ContiguousEnum.Zero.IsInRange(ContiguousEnum.One, ContiguousEnum.One));
            Assert.False(ContiguousEnum.Zero.IsInRange(ContiguousEnum.NegativeOne, ContiguousEnum.NegativeOne));
        }

        [Test]
        public void EnumUtility_ToEnumeratedCollectionIndex()
        {
            Assert.AreEqual(1, ContiguousEnum.Zero.AsCollectionIndex());
            Assert.AreEqual(2, ContiguousEnum.One.AsCollectionIndex());
        }
    }
}
