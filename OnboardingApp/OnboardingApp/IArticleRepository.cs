using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IArticleRepository
{
    IEnumerable<Article> GetAllArticles();
    // добавьте дополнительные методы по мере необходимости
}
