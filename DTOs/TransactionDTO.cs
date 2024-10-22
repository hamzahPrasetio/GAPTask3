namespace Task3.DTOs
{
  public class TransactionDTO
  {
      public int TransactionId { get; set; }
      public int CustomerId { get; set; }      
      public int FoodId { get; set; }      
      public int Qty { get; set; }
      public int TotalPrice { get; set; }
      public DateTime TransactionDate { get; set; }
  }
}
