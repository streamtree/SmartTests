# Within Smart Assertions

*Within Smart Assertions* are assertions that ensure the *Act* part of your test takes not longer than specified maximum duration.

1. Before *Act*  
   Starts a `Stopwatch`.
1. After *Act*  
   Stops the `Stopwatch` and ensures its `ElapsedMilliseconds` is lower than the specified maximum duration.

## Remark

For bests results, this assertion as to be the first one (so that only the Act part is measured without any assertion).

We have two *Within* assertions:

* [`Within(long)`](#within_long)
* [`Within(TimeSpan)`](#within_timespan)

<a name="within_long"></a>

## `Within(long)`

```C#
using NUnit.Framework;
using static SmartTests.SmartTest;

[TestFixture]
public class MyClassTest
{
    [Test]
    public void MyMethodTest()
    {
        var mc = new MyClass();

        RunTest( ValidValue.IsValid,
                 () => mc.MyMethod(),
                 SmartAssert.Within( 10 ) );
    }
}
```

In this example, the Smart Assertion ensures that the `Method()` call is no longer than 10ms.

<a name="within_timespan"></a>

## `Within(TimeSpan)`

```C#
using NUnit.Framework;
using static SmartTests.SmartTest;

[TestFixture]
public class MyClassTest
{
    [Test]
    public void MyMethodTest()
    {
        var mc = new MyClass();

        RunTest( ValidValue.IsValid,
                 () => mc.MyMethod(),
                 SmartAssert.Within( TimeSpan.FromMilliseconds( 10 )  );
    }
}
```

In this example, the Smart Assertion ensures that the `Method()` call is no longer than 10ms.
