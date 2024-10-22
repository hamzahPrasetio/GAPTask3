namespace Task3.Models
{
  public class Transaction
  {
      public int TransactionId { get; set; }
      public int CustomerId { get; set; }
      public Customer Customer { get; set; }
      
      public int FoodId { get; set; }
      public Food Food { get; set; }
      
      public int Qty { get; set; }
      public int TotalPrice { get; set; }
      public DateTime TransactionDate { get; set; }
  }
}
