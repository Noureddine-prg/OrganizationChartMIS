using OrganizationChartMIS.Data.Repositories.Position;
using PositionObject = OrganizationChartMIS.Data.Models.Position;


namespace OrganizationChartMIS.Data.Service.Position
{
    public class PositionService : IPositionService
    {
        private readonly PositionRepository _positionRepository;

        public PositionService(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public PositionObject CreateAndSavePosition(string name, int level, string departmentId, string reportsTo = null, string teamId = null)
        {
            var poid = GenerateUniquePoid();
            Console.WriteLine($"CreateAndSavePosition - Creating Position: Name={name}, Level={level}, DepartmentID={departmentId}, ReportsTo={reportsTo}, TeamID={teamId}, POID={poid}");

            var newPosition = new PositionObject
            {
                Poid = poid,
                Name = name,
                Level = level,
                DepartmentId = departmentId,
                ReportsTo = reportsTo,
                TeamId = teamId
            };

            _positionRepository.AddPosition(newPosition);
            Console.WriteLine($"CreateAndSavePosition - Position Added: {newPosition.Name}");

            return newPosition;
        }

        public PositionObject GetPosition(string poid)
        {
            try
            {
                return _positionRepository.GetPosition(poid);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosition - Exception: {ex.Message}");
                return null;
            }
        }

        public PositionObject UpdatePosition(string poid, string name, int level, string departmentId, string reportsTo = null, string teamId = null)
        {
            try
            {
                var positionToUpdate = _positionRepository.GetPosition(poid);

                if (positionToUpdate != null)
                {
                    positionToUpdate.Name = name;
                    positionToUpdate.Level = level;
                    positionToUpdate.DepartmentId = departmentId;
                    positionToUpdate.ReportsTo = reportsTo;
                    positionToUpdate.TeamId = teamId;

                    _positionRepository.UpdatePosition(positionToUpdate);
                    return positionToUpdate;
                }
                else
                {
                    Console.WriteLine("UpdatePosition - Position not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdatePosition - Exception: {ex.Message}");
                return null;
            }
        }

        public bool DeletePosition(string poid)
        {
            try
            {
                var positionToDelete = _positionRepository.GetPosition(poid);
                if (positionToDelete != null)
                {
                    _positionRepository.DeletePosition(poid);
                    return true;
                }
                else
                {
                    Console.WriteLine("DeletePosition - Position not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeletePosition - Exception: {ex.Message}");
                return false;
            }
        }

        public List<PositionObject> GetAllPositions()
        {
            try
            {
                return _positionRepository.GetAllPositions().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetAllPositions - Exception: {ex.Message}");
                return new List<PositionObject>();
            }
        }

            public List<PositionObject> GetPositionsByDepartment(string department)
            {
                try
                {
                    //Console.WriteLine(department);
                    //Console.WriteLine(_positionRepository.GetPositionsByDepartment(department).ToList());
                    return _positionRepository.GetPositionsByDepartment(department).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"GetPositionsByDepartment - Exception: {ex.Message}");
                    return new List<PositionObject>();
                }
            }

        public string GenerateUniquePoid()
        {
            string poid;
            bool exists;

            do
            {
                var randomNumber = new Random().Next(100000, 999999);
                poid = $"P{randomNumber}";
                exists = _positionRepository.CheckPoidExists(poid);
            }
            while (exists);

            return poid;
        }


    }
}
