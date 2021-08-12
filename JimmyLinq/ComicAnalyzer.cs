using System;
using System.Collections.Generic;
using System.Linq;

namespace JimmyLinq
{
    public static class ComicAnalyzer
    {
        private static PriceRange CalculatePriceRange(Comic comic,IReadOnlyDictionary<int,decimal> prices)
        {
            if (prices[comic.Issue] < 100)
                return PriceRange.Cheap;
            else
                return PriceRange.Expensive;
        }

        public static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(
            IEnumerable<Comic> comics, IReadOnlyDictionary<int, decimal> prices)
        {
            var comicGrouping =
                from comic in comics
                orderby prices[comic.Issue]
                group comic by CalculatePriceRange(comic,prices) into comicGroup
                select comicGroup;

            return comicGrouping;
        }

        public static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews)
        {
            var comicGrouping =
                from comic in comics
                orderby comic.Issue
                join review in reviews
                on comic.Issue equals review.Issue
                select $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {Math.Round(review.Score,2):0.00}";

            return comicGrouping;

        }
    }
}