﻿

using API.INFRASTRUCTURE;

namespace API.InterFace
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        void Save();
    }
}
