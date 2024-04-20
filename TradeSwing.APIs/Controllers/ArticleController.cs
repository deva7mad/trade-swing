using TradeSwing.Domain.Entities;
using TradeSwing.Infrastructure.Data;

namespace TradeSwing.APIs.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


[Route("api/v1/[controller]s")]
public class ArticleController(AppDbContext context) : ApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleEntity>>> GetArticles()
    {
        return await context.Articles.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ArticleEntity>> GetArticle(Guid id)
    {
        var article = await context.Articles.FindAsync(id);

        if (article == null)
            return NotFound();

        return article;
    }

    [HttpPost]
    public async Task<ActionResult<ArticleEntity>> PostArticle(ArticleEntity article)
    {
        article.CreatedAt = DateTime.UtcNow;
        article.Id = Guid.NewGuid();
        context.Articles.Add(article);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetArticle", new { id = article.Id }, article);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutArticle(Guid id, ArticleEntity article)
    {
        if (id != article.Id)
            return BadRequest();

        context.Entry(article).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteArticle(Guid id)
    {
        var article = await context.Articles.FindAsync(id);

        if (article == null)
            return NotFound();

        context.Articles.Remove(article);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArticleExists(Guid id)
    {
        return context.Articles.Any(e => e.Id == id);
    }
}