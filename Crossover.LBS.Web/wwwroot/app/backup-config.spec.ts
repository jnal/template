import { BackupConfig } from "./backup-config";

describe('BackupConfig', () => {
    it('has machineid', () => {
        let config = new BackupConfig();
        config.machineId = 1;
        expect(config.machineId).toBe(1);
    });

});