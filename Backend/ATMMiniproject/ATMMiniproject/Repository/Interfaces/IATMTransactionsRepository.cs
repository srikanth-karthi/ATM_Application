﻿using ATM_MiniProject.Models;

namespace ATMMiniproject.Repository.Interfaces
{
    public interface IATMTransactionsRepository : IRepository<int, ATMTransaction>
    {
         Task<IList<ATMTransaction>> GetAllByAcctId(int AcctId);
    }
}
