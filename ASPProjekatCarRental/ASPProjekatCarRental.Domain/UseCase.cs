namespace ASPProjekatCarRental.Domain
{
    public class UseCase : Entity
    {
        public string UseCaseName { get; set; }

        public virtual ICollection<UserUseCase> UserUseCase { get; set; } = new List<UserUseCase>();
    }
}