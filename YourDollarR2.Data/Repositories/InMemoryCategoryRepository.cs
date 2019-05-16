using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YourDollarR2.Core;

namespace YourDollarR2.DataAccess.Repositories
{
    public class InMemoryCategoryRepository : IBudgetCategoryRepository
    {
        private readonly List<BudgetCategory> _inMemoryCategories;
        public InMemoryCategoryRepository()
        {
            _inMemoryCategories = new List<BudgetCategory>
            {
                new BudgetCategory
                {
                    ShortName = "Shelter",
                    Description = "Place your rent, mortgage, household taxes, and HOA fees here."
                },
                new BudgetCategory
                {
                    ShortName = "Utilities",
                    Description = "Place your electricity, water, heating, phones, cable, internet bills here."
                },
                new BudgetCategory
                {
                    ShortName = "Clothing",
                    Description = "Place adult and children clothing expenses here."
                },
                new BudgetCategory
                {
                    ShortName = "Transportation",
                    Description = "Place fuel, vehicle repairs, oil changes, public transportation fees here."
                }
            };
        }

        public BudgetCategory AddCategory(BudgetCategory category)
        {
            _inMemoryCategories.Add(category);
            return category;
        }

        public BudgetCategory DeleteCategory(Guid categoryId)
        {
            var categoryFromStore = GetCategoryById(categoryId);
            if (categoryFromStore != null)
            {
                _inMemoryCategories.Remove(categoryFromStore);
            }

            return categoryFromStore;
        }

        public IEnumerable<BudgetCategory> GetCategoriesByName(string shortName = null)
        {
            return from c in _inMemoryCategories
                where string.IsNullOrWhiteSpace(shortName) || c.ShortName.Contains(shortName)
                orderby c.ShortName
                select c;
        }

        public BudgetCategory GetCategoryById(Guid categoryId)
        {
            return _inMemoryCategories.FirstOrDefault(c => c.Id == categoryId);
        }

        public bool SaveChanges()
        {
            return true;
        }

        public BudgetCategory UpdateCategory(BudgetCategory category)
        {
            var categoryFromStore = GetCategoryById(category.Id);
            if (categoryFromStore != null)
            {
                categoryFromStore.ShortName = category.ShortName;
                categoryFromStore.Description = category.Description;
            }

            return categoryFromStore;
        }
    }
}
