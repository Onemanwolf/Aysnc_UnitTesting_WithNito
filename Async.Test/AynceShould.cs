using System;
using System.Threading.Tasks;
using Xunit;
using AsyncExamples;
using FluentAssertions;
using Nito.AsyncEx;

namespace Async.Test
{
    public class AynceShould
    {
        [Fact]
        public async Task SomethingAsync_Throws_Exception()
        {
            var sut = new AsynchronousError();
            await Assert.ThrowsAsync<InvalidOperationException>(async () => {
                await sut.SomethingAsync();
            });
        }

        [Fact]
        public async Task SomethingAsync_Return_13()
        {
            var sut = new AsynchronousSuccess();

         var results =  await sut.SomethingAsync();

            results.Should().Be(13);

        }

        [Fact]
        public void SomethingAsync_Return_13_AsyncContext()
        {
            var sut = new AsynchronousSuccess();
           
            AsyncContext.Run(async () =>
            {
                var results = await sut.SomethingAsync();

                results.Should().Be(13);

            });
          

        }

        [Fact]
        public void SomethingAsync_Return_Exception_AsyncContext()
        {
            var sut = new AsynchronousError();


            // using Nito.AsyncEx; is a help libary for async 
            // it can help with testing async methods to avoid deadlocks a race conditions
            AsyncContext.Run(async () =>
            {
               
                await Assert.ThrowsAsync<InvalidOperationException>(async () => {
                    
                    // Do not forget to await the task returned by ThrowAsync! 
                    // If you forget to you test may pass silently a false positive 
                    await sut.SomethingAsync();
                });

            });


        }


        [Fact]
        public void AsyncVoidTest()
        {
            {
                var sut = new AsynchronousVoid();

                // if you can not refactor the void method out test with Nito
                AsyncContext.Run(async () =>
                {
                    sut.AsyncVoidMethod();

                    sut.value.Should().Be("stringValue");

                });


            }
        }

        [Fact]
        public void AsyncTaskTest()
        {
            {
                var sut = new AsynchronousTask();

     
                AsyncContext.Run(async () =>
                {
                     await sut.AsyncTaskMethod();

                    sut.value.Should().Be("stringValue");

                });


            }
        }

        [Fact]
        public void AsyncTaskReturnsBoolTest()
        {
            {
                var sut = new AsynchronousTask();


                AsyncContext.Run(async () =>
                {
                   var result = await sut.AsyncTaskMethodReturnsBool();

                    result.Should().BeTrue();

                });


            }
        }
    }




}
