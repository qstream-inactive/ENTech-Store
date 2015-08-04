using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }

        public FileBaseDto Photo { get; set; }
	}
}