using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ArticleRepository : IArticleRepository
{
    private List<Article> _articles;

    public ArticleRepository()
    {
        // Здесь вы можете заполнить свои статьи
        _articles = new List<Article>
        {
            new Article { Title = "Инструкция 1", Category = "Инструкции", CreatedDate = DateTime.Now, PdfFilePath = "path/to/document1.pdf" },
            new Article { Title = "Правила 1", Category = "Правила", CreatedDate = DateTime.Now, PdfFilePath = "path/to/document2.pdf" },
            // добавьте другие статьи
        };
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _articles;
    }
}

