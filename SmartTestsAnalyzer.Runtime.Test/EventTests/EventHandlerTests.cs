﻿using System;

using NUnit.Framework;

using SmartTests;
using SmartTests.Assertions;
using SmartTests.Criterias;

using SmartTestsAnalyzer.Runtime.Test.Annotations;

using static SmartTests.SmartTest;



namespace SmartTestsAnalyzer.Runtime.Test.EventTests
{
    [TestFixture]
    public class EventHandlerTests
    {
        public class MyClass
        {
            public MyClass( bool raiseEvent )
            {
                _RaiseEvent = raiseEvent;
            }


            private readonly bool _RaiseEvent;


            public void Method()
            {
                if( _RaiseEvent )
                    RaiseMyevent();
            }


            public event EventHandler MyEvent;


            [NotifyPropertyChangedInvocator]
            protected virtual void RaiseMyevent() => MyEvent?.Invoke( this, EventArgs.Empty );
        }


        [Test]
        public void Raised()
        {
            var mc = new MyClass( true );

            RunTest( AnyValue.IsValid,
                     () => mc.Method(),
                     SmartAssert.Raised( mc, "MyEvent" ) );
        }


        [Test]
        public void NotEvent()
        {
            var exception = Assert.Catch<BadTestException>( () =>
                                                            {
                                                                var mc = new MyClass( true );

                                                                RunTest( AnyValue.IsValid,
                                                                         () => mc.Method(),
                                                                         SmartAssert.Raised( mc, "NotEvent" ) );
                                                            } );
            Assert.AreEqual( "BAD TEST: 'NotEvent' is not an event of type 'EventHandlerTests+MyClass'", exception.Message );
        }


        [Test]
        public void NotRaised()
        {
            var exception = Assert.Catch<SmartTestException>( () =>
                                                              {
                                                                  var mc = new MyClass( false );

                                                                  RunTest( AnyValue.IsValid,
                                                                           () => mc.Method(),
                                                                           SmartAssert.Raised( mc, "MyEvent" ) );
                                                              } );

            Assert.AreEqual( "Event 'EventHandlerTests+MyClass.MyEvent' was expected", exception.Message );
        }


        [Test]
        public void UnexpectedRaised()
        {
            var exception = Assert.Catch<SmartTestException>( () =>
                                                              {
                                                                  var mc = new MyClass( true );

                                                                  RunTest( AnyValue.IsValid,
                                                                           () => mc.Method(),
                                                                           SmartAssert.NotRaised( mc, "MyEvent" ) );
                                                              } );

            Assert.AreEqual( "Event 'EventHandlerTests+MyClass.MyEvent' was unexpected", exception.Message );
        }


        [Test]
        public void UnexpectedNotEvent()
        {
            var exception = Assert.Catch<BadTestException>( () =>
                                                            {
                                                                var mc = new MyClass( true );

                                                                RunTest( AnyValue.IsValid,
                                                                         () => mc.Method(),
                                                                         SmartAssert.NotRaised( mc, "NotEvent" ) );
                                                            } );
            Assert.AreEqual( "BAD TEST: 'NotEvent' is not an event of type 'EventHandlerTests+MyClass'", exception.Message );
        }


        [Test]
        public void UnexpectedNotRaised()
        {
            var mc = new MyClass( false );

            RunTest( AnyValue.IsValid,
                     () => mc.Method(),
                     SmartAssert.NotRaised( mc, "MyEvent" ) );
        }
    }
}