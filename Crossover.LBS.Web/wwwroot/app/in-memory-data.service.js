"use strict";
var InMemoryDataService = (function () {
    function InMemoryDataService() {
    }
    InMemoryDataService.prototype.createDb = function () {
        var machines = [
            {
                id: 11,
                ipAddress: '127.0.0.1',
                isActive: true,
                backupConfigs: [
                    { id: 11, source: 'c:\\test', destination: '\\\\test', schedule: '11-27-2016', lastRunTime: '11-27-2016' }
                ]
            }
        ];
        var backupConfigs = [
            { id: 11, sourcePath: 'c:\\test', destinationPath: '\\\\test', schedule: '11-27-2016', sourceUsername: 'source' }
        ];
        var backupLogs = [
            { message: 'started copying files from c:\\source to c:\\dest by schedule', date: '11-27-2016', backupConfigId: 11 }
        ];
        return { machines: machines, backupConfigs: backupConfigs, backupLogs: backupLogs };
    };
    return InMemoryDataService;
}());
exports.InMemoryDataService = InMemoryDataService;
/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/ 
//# sourceMappingURL=in-memory-data.service.js.map