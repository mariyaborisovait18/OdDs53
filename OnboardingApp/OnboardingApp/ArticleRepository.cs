using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics; // Для Process
using System.IO;         // Для Path
using System.Text;
using System.Threading.Tasks;

public class ArticleRepository : IArticleRepository
{
    private List<Article> _articles;

    public ArticleRepository()
    {
        // Заполнение списка с правильной инициализацией
        _articles = new List<Article>
        {
            new Article("Инструкция 1", "Инструкции") { PdfFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "PDF", "Учебник по с++ Стефан Р. Дэвис.pdf") },
            new Article("Правила 1", "Правила") { PdfFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "PDF", "Учебник по с# М.А.Медведев.pdf") },
            // добавьте другие статьи
        };
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _articles;
    }

    public void AddArticle(Article article)
    {
        _articles.Add(article);
    }

    public void RemoveArticle(Article article)
    {
        _articles.Remove(article);
    }

    public void UpdateArticle(Article article)
    {
        var existingArticle = _articles.FirstOrDefault(a => a.Title == article.Title);
        if (existingArticle != null)
        {
            existingArticle.Category = article.Category;
            existingArticle.CreatedDate = article.CreatedDate;
            existingArticle.PdfFilePath = article.PdfFilePath;
        }
    }
}
