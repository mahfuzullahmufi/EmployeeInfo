using LinqToDB.Data;
using LinqToDB;
using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Repository.Data
{
    public class Linq2DbDataConnection : DataConnection
    {
        public Linq2DbDataConnection(DataOptions<Linq2DbDataConnection> options) : base(options.Options) { }

        public ITable<Employee> Employees => this.GetTable<Employee>();
        public ITable<Address> Addresses => this.GetTable<Address>();
        public ITable<Hobby> Hobbies => this.GetTable<Hobby>();
        public ITable<Project> Projects => this.GetTable<Project>();
        public ITable<EmployeeProject> EmployeeProjects  => this.GetTable<EmployeeProject>();
    }
}

