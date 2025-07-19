namespace TimeTravelTests
{
    public class TimeTravelTests
    {
        [Fact]
        public void SetAndGet_ShouldReturnCorrectValueAtGivenTimestamp()
        {
            var tt = new TimeTravel();
            tt.Set("A", "Apple");
            tt.Set("A", "Avocado");

            var result = tt.Get("A", 1);
            Assert.Equal("Apple", result);
        }

        [Fact]
        public void Rollback_ShouldAffectSubsequentGet()
        {
            var tt = new TimeTravel();
            tt.Set("A", "Alpha");
            tt.Set("A", "Beta");

            tt.Rollback(1);
            var result = tt.Get("A", 999); // Should use timestamp 1 due to rollback

            Assert.Equal("Alpha", result);
        }

        [Fact]
        public void ChangedBetween_ShouldReturnOnlyChangedKeys()
        {
            var tt = new TimeTravel();
            tt.Set("A", "Alpha"); // 1
            tt.Set("B", "Bravo"); // 2
            tt.Set("A", "Almond"); // 3

            var changed = tt.ChangedBetween(1, 3);
            Assert.Contains("A", changed);
            Assert.DoesNotContain("B", changed);
        }
    }
}