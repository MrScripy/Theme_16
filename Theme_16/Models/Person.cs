using System.Collections.ObjectModel;

namespace Theme_16.Models
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Patronymic { get; set; }= string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Mail { get; set; } = string.Empty;

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
    }
}
