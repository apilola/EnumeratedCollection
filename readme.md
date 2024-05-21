# Enumerated Collection

## Overview

Lookup tables are a common technique used to store and retrieve data efficiently based on a set of keys. They are particularly useful for tasks requiring quick access to data, such as in games, simulations, or any application where performance is critical. The Enumemerated Collection class attempts to fill the gap that exists in c# collections api for building look up tables. 

#### Arrays Are Not enough
```
enum ShapeType
{
    Square,
    Triangle,
    Elipse
}

static readonly float[] AreaCoefficientTable = 
{
    1, //Square
    0.5, //Triangle
    1.57079632679 // Elipse (Pi divided by 2)
};

float static CalculateArea(ShapeType shapeType, float width, float height)
{
    return width * height * AreaCoefficientTable[(int)shapeType];
}
```

Arrays like in the example above are often combined with enums to implement lookup tables because enums provide a clear, readable way to define a set of possible keys, while arrays offer fast access times due to their contiguous memory allocation. However, while arrays are performant, they lack a user-friendly interface for lookup operations, especially when compared to dictionaries. 

#### Dictionaries Leave A Lot To Be Desired
```
static readonly Dictionary<ShapeType, float> AreaCoefficientDict = new ()
{
    { ShapeType.Square, 1 },
    { ShapeType.Triangle, 0.5 },
    { ShapeType.Elipse, 1.57079632679 } // Pi divided by 2
};

float static CalculateArea(ShapeType shapeType, float width, float height)
{
    return width * height * AreaCoefficientDict[shapeType];
}
```
Dictionaries in C# provide a more intuitive interface for defining lookup tables, allowing for key-value pairs where keys can be of any type, including enums. This makes code more readable and easier to maintain. Imagine a case in our Shape Type example where the order of the shape type enumerations are changed. Arrays would no longer provide the expected coefficient, while a dictionary exhibit would continue to provide the expected result, no changes nessessary. However, dictionaries have performance drawbacks in the context of lookup tables due to the overhead of hashing and handling collisions, which can be significant in performance-critical applications.

#### Enumerated Collection Provides a Solution
```
static readonly EnumeratedCollection<ShapeType, float> AreaCoefficientDict = new ()
{
    { ShapeType.Square, 1 },
    { ShapeType.Triangle, 0.5 },
    { ShapeType.Elipse, 1.57079632679 } // Pi divided by 2
}.MakeReadonly();

float sttic CalculateArea(ShapeType shapeType, float width, float height)
{
    return width * height * AreaCoefficientDict[shapeType];
}
```
The Enumerated Collection library aims to provide a clean wrapper around the default array functionality, combining the performance benefits of arrays with the usability of dictionaries. By using enums as keys and arrays for storage, Enumerated Collection offers a solution that maintains high performance while providing a more user-friendly application programming interface than raw arrays.

## Benchmarks
The following benchmark results demonstrate the performance of different lookup table implementations. The Enumerated Collection consistently provides performance close to raw arrays and significantly better than dictionaries: (Lower is better)

| Method             | Mean      | Error    | StdDev   |
|------------------- |----------:|---------:|---------:|
| Enum8_Array        |  17.83 ns | 0.196 ns | 0.183 ns |
| Enum8_Dictionary   |  28.36 ns | 0.194 ns | 0.162 ns |
| Enum8_Collection   |  18.74 ns | 0.103 ns | 0.086 ns |
| Enum32_Array       |  43.87 ns | 0.356 ns | 0.333 ns |
| Enum32_Dictionary  |  92.20 ns | 0.373 ns | 0.349 ns |
| Enum32_Collection  |  47.13 ns | 0.191 ns | 0.179 ns |
| Enum256_Array      | 292.27 ns | 0.983 ns | 0.919 ns |
| Enum256_Dictionary | 690.88 ns | 1.951 ns | 1.825 ns |
| Enum256_Collection | 322.67 ns | 0.900 ns | 0.841 ns |

These results illustrate that while dictionaries provide an easy-to-use interface, their performance is significantly worse than arrays and Enumerated Collection, especially as the number of enum values increases. Enumerated Collection maintains performance close to arrays, making it a suitable choice for performance-critical applications requiring both efficiency and ease of use.