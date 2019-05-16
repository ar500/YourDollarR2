using System;
using System.Collections.Generic;
using System.Linq;
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
                    Id = Guid.Parse("8f55f71b-8488-4599-9a9a-ab4b53e31230"),
                    ShortName = "Shelter",
                    Description = "Place your rent, mortgage, household taxes, and HOA fees here."
                },
                new BudgetCategory
                {
                    Id = Guid.Parse("3bd389d7-74a6-4e0b-b59c-dbb0dec4e3d1"),
                    ShortName = "Utilities",
                    Description = "Place your electricity, water, heating, phones, cable, internet bills here."
                },
                new BudgetCategory
                {
                    Id = Guid.Parse("9c0e186a-79ea-4d6b-8359-e673c98eba17"),
                    ShortName = "Clothing",
                    Description = "Place adult and children clothing expenses here."
                },
                new BudgetCategory
                {
                    Id = Guid.Parse("8a2f88c1-0a65-4127-8fae-c2a06243c4aa"),
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
