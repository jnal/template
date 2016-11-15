import { BackupConfig } from './backup-config';
import {Machine} from './machine'

export class MachineBackups {
    machine: Machine[];
    backupConfigs: BackupConfig[];
}
