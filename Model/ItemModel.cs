using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardWorker.Model
{
    /// <summary>
    /// Класс модели данных. <br/>
    /// Используйте <see cref="ModelFactory"/> для создания экземпляров.
    /// </summary>
    public class ItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public ItemModel(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
