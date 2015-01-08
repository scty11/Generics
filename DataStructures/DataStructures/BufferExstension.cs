using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public static class BufferExstension
    {
        public static void Dump<T>(this IBuffer<T> buffer, Action<T> print)
        {
            foreach (var item in buffer)
            {
                print(item);
            }
            
        }
        //a generic method must indicate the return type
        //after the method name.
        public static IEnumerable<TOutput> Map<T,TOutput>
            (this IBuffer<T> buffer, Converter<T,TOutput> convert)
        {
            //basically craeting our own select linq operator
            //just included as an example
            return buffer.Select(d => convert(d));
        }
    }
}
