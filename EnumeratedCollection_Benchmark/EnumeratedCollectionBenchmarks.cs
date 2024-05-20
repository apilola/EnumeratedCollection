// See https://aka.ms/new-console-template for more information
using AP.Collections;
using AP.Utilities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

public class EnumeratedCollectionBenchmarks
{
    public struct TestData
    {
        public bool BoolValue;
        public int IntValue;
    }

    public class TestCase<T> where T:Enum
    {
        public TestCase()
        {
            var length = EnumUtility<T>.Keys.Count;
            Array = new TestData[length];
            Dictionary = new Dictionary<T, TestData>(length);
            Collection = new EnumeratedCollection<T, TestData>();

            for (var i = 0; i < length; i++)
            {
                var data = new TestData()
                {
                    BoolValue = i % 2 == 0,
                    IntValue = i
                };
                Collection.Add(EnumUtility<T>.Keys[i], data);
                Dictionary.Add(EnumUtility<T>.Keys[i], data);
            }
        }
        public readonly Dictionary<T, TestData> Dictionary;
        public readonly EnumeratedCollection<T, TestData> Collection;
        public readonly TestData[] Array;
    }

    private TestCase<Enum8> _enum8TestCase;
    private TestCase<Enum32> _enum32TestCase;
    private TestCase<Enum256> _enum256TestCase;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _enum8TestCase = new TestCase<Enum8>();
        _enum32TestCase = new TestCase<Enum32>();
        _enum256TestCase = new TestCase<Enum256>();
    }

    [Benchmark()]
    public void Enum8_Array()
    {
        var array = _enum8TestCase.Array;
        int sum = 0;
        foreach (var key in EnumUtility<Enum8>.Keys)
        {
            var value = array[(int) key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum8_Dictionary()
    {
        int sum = 0;
        var dictionary = _enum8TestCase.Dictionary;
        foreach (var key in EnumUtility<Enum8>.Keys)
        {
            var value = dictionary[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum8_Collection()
    {
        int sum = 0;
        var collection = _enum8TestCase.Collection;
        foreach (var key in EnumUtility<Enum8>.Keys)
        {
            var value = collection[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum32_Array()
    {
        var array = _enum32TestCase.Array;
        int sum = 0;
        foreach (var key in EnumUtility<Enum32>.Keys)
        {
            var value = array[(int) key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum32_Dictionary()
    {
        int sum = 0;
        var dictionary = _enum32TestCase.Dictionary;
        foreach (var key in EnumUtility<Enum32>.Keys)
        {
            var value = dictionary[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum32_Collection()
    {
        int sum = 0;
        var collection = _enum32TestCase.Collection;
        foreach (var key in EnumUtility<Enum32>.Keys)
        {
            var value = collection[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum256_Array()
    {
        var array = _enum256TestCase.Array;
        int sum = 0;
        foreach (var key in EnumUtility<Enum256>.Keys)
        {
            var value = array[(int) key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum256_Dictionary()
    {
        int sum = 0;
        var dictionary = _enum256TestCase.Dictionary;
        foreach (var key in EnumUtility<Enum256>.Keys)
        {
            var value = dictionary[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }

    [Benchmark()]
    public void Enum256_Collection()
    {
        int sum = 0;
        var collection = _enum256TestCase.Collection;
        foreach (var key in EnumUtility<Enum256>.Keys)
        {
            var value = collection[key];
            if (value.BoolValue)
                sum += value.IntValue;
        }
        if (sum < 0)
            Console.WriteLine("Sum Was less than 0");
    }


    public enum Enum8
    {
        Value0,
        Value1,
        Value2,
        Value3,
        Value4,
        Value5,
        Value6,
        Value7,
    }

    public enum Enum32
    {
        Value0,
        Value1,
        Value2,
        Value3,
        Value4,
        Value5,
        Value6,
        Value7,
        Value8,
        Value9,
        Value10,
        Value11,
        Value12,
        Value13,
        Value14,
        Value15,
        Value16,
        Value17,
        Value18,
        Value19,
        Value20,
        Value21,
        Value22,
        Value23,
        Value24,
        Value25,
        Value26,
        Value27,
        Value28,
        Value29,
        Value30,
        Value31,
    }

    public enum Enum256
    {
        Value0,
        Value1,
        Value2,
        Value3,
        Value4,
        Value5,
        Value6,
        Value7,
        Value8,
        Value9,
        Value10,
        Value11,
        Value12,
        Value13,
        Value14,
        Value15,
        Value16,
        Value17,
        Value18,
        Value19,
        Value20,
        Value21,
        Value22,
        Value23,
        Value24,
        Value25,
        Value26,
        Value27,
        Value28,
        Value29,
        Value30,
        Value31,
        Value32,
        Value33,
        Value34,
        Value35,
        Value36,
        Value37,
        Value38,
        Value39,
        Value40,
        Value41,
        Value42,
        Value43,
        Value44,
        Value45,
        Value46,
        Value47,
        Value48,
        Value49,
        Value50,
        Value51,
        Value52,
        Value53,
        Value54,
        Value55,
        Value56,
        Value57,
        Value58,
        Value59,
        Value60,
        Value61,
        Value62,
        Value63,
        Value64,
        Value65,
        Value66,
        Value67,
        Value68,
        Value69,
        Value70,
        Value71,
        Value72,
        Value73,
        Value74,
        Value75,
        Value76,
        Value77,
        Value78,
        Value79,
        Value80,
        Value81,
        Value82,
        Value83,
        Value84,
        Value85,
        Value86,
        Value87,
        Value88,
        Value89,
        Value90,
        Value91,
        Value92,
        Value93,
        Value94,
        Value95,
        Value96,
        Value97,
        Value98,
        Value99,
        Value100,
        Value101,
        Value102,
        Value103,
        Value104,
        Value105,
        Value106,
        Value107,
        Value108,
        Value109,
        Value110,
        Value111,
        Value112,
        Value113,
        Value114,
        Value115,
        Value116,
        Value117,
        Value118,
        Value119,
        Value120,
        Value121,
        Value122,
        Value123,
        Value124,
        Value125,
        Value126,
        Value127,
        Value128,
        Value129,
        Value130,
        Value131,
        Value132,
        Value133,
        Value134,
        Value135,
        Value136,
        Value137,
        Value138,
        Value139,
        Value140,
        Value141,
        Value142,
        Value143,
        Value144,
        Value145,
        Value146,
        Value147,
        Value148,
        Value149,
        Value150,
        Value151,
        Value152,
        Value153,
        Value154,
        Value155,
        Value156,
        Value157,
        Value158,
        Value159,
        Value160,
        Value161,
        Value162,
        Value163,
        Value164,
        Value165,
        Value166,
        Value167,
        Value168,
        Value169,
        Value170,
        Value171,
        Value172,
        Value173,
        Value174,
        Value175,
        Value176,
        Value177,
        Value178,
        Value179,
        Value180,
        Value181,
        Value182,
        Value183,
        Value184,
        Value185,
        Value186,
        Value187,
        Value188,
        Value189,
        Value190,
        Value191,
        Value192,
        Value193,
        Value194,
        Value195,
        Value196,
        Value197,
        Value198,
        Value199,
        Value200,
        Value201,
        Value202,
        Value203,
        Value204,
        Value205,
        Value206,
        Value207,
        Value208,
        Value209,
        Value210,
        Value211,
        Value212,
        Value213,
        Value214,
        Value215,
        Value216,
        Value217,
        Value218,
        Value219,
        Value220,
        Value221,
        Value222,
        Value223,
        Value224,
        Value225,
        Value226,
        Value227,
        Value228,
        Value229,
        Value230,
        Value231,
        Value232,
        Value233,
        Value234,
        Value235,
        Value236,
        Value237,
        Value238,
        Value239,
        Value240,
        Value241,
        Value242,
        Value243,
        Value244,
        Value245,
        Value246,
        Value247,
        Value248,
        Value249,
        Value250,
        Value251,
        Value252,
        Value253,
        Value254,
        Value255,
    }
}