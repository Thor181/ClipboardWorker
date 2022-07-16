using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardWorker.Model
{
    public static class ModelFactory
    {
        private static int _countInstance = 0;

        public static ItemModel GetItemModel(string text)
        {
            _countInstance++;
            return new ItemModel(_countInstance, text);
        }
    }
}
