﻿using BionicCoreLibrary.Core;
using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.GenereicRepository;

namespace BionicCoreLibrary.DapperRepository.Repositries.BaseRepository
{
    public interface IUserDapperRepository : IGenericDapperRepository<Users>
    {
    }
    public class UserDapperRepository : GenericDapperRepository<Users>, IUserDapperRepository
    {
        public UserDapperRepository(BionicSqlKataConnecton bionicSqlKataConnecton, SecondarySqlKataConnection secondarySqlKataConnection) : base(bionicSqlKataConnecton, secondarySqlKataConnection)
        {
        }
    }
}
