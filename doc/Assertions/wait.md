# Wait Smart Assertions

*Wait Smart Assertions* are assertions that wait for a handle set in the *Act* part of your test.

1. Before *Act*  
   Creates an implicit `AutoResetEvent` in the Act Context, if none is specified.
1. After *Act*  
   Waits for the handle (specified or implicit) for the maximum specified amount of time. If this timeout is reached, the assertion fails.

We have four *Wait* assertions:

* [`Wait(WaitHandle,double)`](#wait_waithandle_double)
* [`Wait(WaitHandle,TimeSpan)`](#wait_waithandle_timespan)
* [`WaitContextHandle(double)`](#waitcontexthandle_double)
* [`WaitContextHandle(TimeSpan)`](#waitcontexthandle_timespan)

<a name="wait_long"></a>

## `Wait(WaitHandle,double)`

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
        var handle = new ManualResetEvent( false );

        RunTest( ValidValue.IsValid,
                 () => mc.MyMethod( () => handle.set() ),
                 SmartAssert.Wait( handle, 1000 ) );
    }
}
```

In this example, the Smart Assertion ensures that the call to `MyMethod` calls the provided argument within 1000ms.

## `Wait(WaitHandle,TimeSpan)`

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
        var handle = new ManualResetEvent( false );

        RunTest( ValidValue.IsValid,
                 () => mc.MyMethod( () => handle.set() ),
                 SmartAssert.Wait( handle, TimeSpan.FromSeconds( 1 ) ) );

        Assert.IsTrue( mc.Done ); // The method runs the parallel code (ctx.SetHandle) to its end.
        Assert.IsNull( mc.Exception ); // There was no exception in the parallel code thread.
    }
}
```

In this example, the Smart Assertion ensures that the call to `MyMethod` calls the provided argument within 1s.

<a name="waitcontexthandle_double"></a>

## `WaitContextHandle(double)`

```C#
using NUnit.Framework;
using static SmartTests.SmartTest;

[TestFixture]
public class MyClassTest
{
    [Test]
    public void MyMethodTest()
    {
       var mc = new MyClass(300);

        RunTest( AnyValue.IsValid,
                ctx => mc.Method( ctx.SetHandle ),
                SmartAssert.Within( 100 ),
                SmartAssert.WaitContextHandle( 1000 ) );

        Assert.IsTrue( mc.Done ); // The method runs the parallel code (ctx.SetHandle) to its end.
        Assert.IsNull( mc.Exception ); // There was no exception in the parallel code thread.
    }
}
```

In this example, the Smart Assertion ensures that the call to `MyMethod` calls the provided argument within 1000ms.

<a name="waitcontexthandle_timespan"></a>

## `WaitContextHandle(TimeSpan)`

```C#
using NUnit.Framework;
using static SmartTests.SmartTest;

[TestFixture]
public class MyClassTest
{
    [Test]
    public void MyMethodTest()
    {
       var mc = new MyClass(300);

        RunTest( AnyValue.IsValid,
                ctx => mc.Method( ctx.SetHandle ),
                SmartAssert.Within( 100 ),
                SmartAssert.WaitContextHandle( TimeSpan.FromSeconds( 1 ) ) );

        Assert.IsTrue( mc.Done ); // The method runs the parallel code (ctx.SetHandle) to its end.
        Assert.IsNull( mc.Exception ); // There was no exception in the parallel code thread.
    }
}
```

In this example, the Smart Assertion ensures that the call to `MyMethod` calls the provided argument within 1s.
