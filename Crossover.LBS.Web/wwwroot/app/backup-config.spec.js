"use strict";
var backup_config_1 = require("./backup-config");
describe('BackupConfig', function () {
    it('has machineid', function () {
        var config = new backup_config_1.BackupConfig();
        config.machineId = 1;
        expect(config.machineId).toBe(1);
    });
});
//# sourceMappingURL=backup-config.spec.js.map