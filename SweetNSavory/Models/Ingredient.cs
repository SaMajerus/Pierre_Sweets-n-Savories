using System.Collections.Generic;
using System;  //This is present in Recipe.cs, but not Category.cs

namespace SweetNSavory.Models
{
  public class Ingredient
  {
    public Ingredient()
    {
      this.JoinRecIng = new HashSet<RecipeIngredient>();
    }

    public int IngredientId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RecipeIngredient> JoinRecIng { get; set; }
  }
}