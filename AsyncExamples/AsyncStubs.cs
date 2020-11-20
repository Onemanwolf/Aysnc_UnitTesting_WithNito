using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExamples
{
    public class AsyncStubs
    {
        
    }

    public interface IMyInterface
    {
        Task<int> SomethingAsync();
    }

    public class SychronousSuccess : IMyInterface
    {
        public Task<int> SomethingAsync()
        {
            return Task.FromResult(13);
        }
    }

    public class AsynchronousError : IMyInterface
    {
        public async Task<int> SomethingAsync()
        {
            return await Task.FromException<int>(new InvalidOperationException());
        }
    }

    public class AsynchronousSuccess : IMyInterface
    {
        public async Task<int> SomethingAsync()
        {
            await Task.Yield();
            return 13;
        }
    }

    public class AsynchronousVoid
    {
        public string value { get; set; }

        public async void AsyncVoidMethod()
        {
            value = "stringValue";
        }
    }

    public class AsynchronousTask
    {
        public string value { get; set; }
        private string privateValue { get; set; }

        public async Task AsyncTaskMethod()
        {
            value = "stringValue";
        }

        public async Task<bool> AsyncTaskMethodReturnsBool()
        {
            try {
               // throw new InvalidOperationException();
                privateValue = "stringValue";
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }


    }


}
