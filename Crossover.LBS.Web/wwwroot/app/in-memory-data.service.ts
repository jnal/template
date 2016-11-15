import { InMemoryDbService } from 'angular-in-memory-web-api';
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
      let machines = [
      {
          id: 11,
          ipAddress: '127.0.0.1',
          isActive: true,
            backupConfigs: [ 
                { id: 11, source: 'c:\\test', destination: '\\\\test', schedule: '11-27-2016', lastRunTime: '11-27-2016' }
            ]
        }
    ];

    let backupConfigs = [
        { id: 11, sourcePath: 'c:\\test', destinationPath: '\\\\test', schedule: '11-27-2016', sourceUsername: 'source' }
    ];

    let backupLogs = [
        { message: 'started copying files from c:\\source to c:\\dest by schedule', date: '11-27-2016', backupConfigId: 11  }
    ];

    return { machines, backupConfigs, backupLogs};
  }
}


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/