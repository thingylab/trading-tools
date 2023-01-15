using DbUp;
using DbUp.Engine;

namespace TradingTools.Db.Migrations;

public class Runner
{
    private readonly string _connectionString;
    private readonly UpgradeEngine _upgrader;

    public Runner(string connectionString)
    {
        _connectionString = connectionString;
        _upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(Runner).Assembly)
            .Build();
    }

    public bool UpgradeRequired()
    {
        return _upgrader.IsUpgradeRequired();
    }

    public bool Upgrade()
    {
        var result = _upgrader.PerformUpgrade();

        return result.Successful;
    }
}
