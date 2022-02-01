namespace UserService.Consumers.GetUsers
{
    public class GetUserByIdQuery
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
