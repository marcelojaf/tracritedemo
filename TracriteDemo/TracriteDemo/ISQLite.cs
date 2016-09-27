using SQLite;
using System;

namespace TracriteDemo
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
