export class BackupConfig {
    id: number;
    machineId: number;
    sourcePath: string;
    sourceDomain: string;
    sourceUsername: string;
    sourcePassword: string;
    destinationPath: string;
    destinationDomain: string;
    destinationUsername: string;
    destinationPassword: string;
    schedule: Date;
    lastRun: Date;
}

