using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Article
{
    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PdfFilePath { get; set; } // Путь к PDF файлу


    public Article(string title, string category)
    {
        Title = title;
        Category = category;
        CreatedDate = DateTime.Now; // или установите дату по вашему усмотрению
    }
}
