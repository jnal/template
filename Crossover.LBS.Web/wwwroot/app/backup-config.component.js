"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var router_1 = require('@angular/router');
var common_1 = require('@angular/common');
var backup_config_1 = require('./backup-config');
var lbs_service_1 = require('./lbs.service');
var BackupConfigComponent = (function () {
    function BackupConfigComponent(lbsService, route, location) {
        this.lbsService = lbsService;
        this.route = route;
        this.location = location;
    }
    BackupConfigComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = +params['id'];
            var method = params['method'];
            if (method === 'update' && id > 0) {
                _this.lbsService.getBackupConfig(id)
                    .then(function (backupConfig) { return _this.backupConfig = backupConfig; });
            }
            else {
                var backupConfig = new backup_config_1.BackupConfig();
                backupConfig.schedule = new Date();
                backupConfig.machineId = id;
                _this.backupConfig = backupConfig;
            }
        });
    };
    BackupConfigComponent.prototype.save = function () {
        var _this = this;
        if (this.backupConfig.id > 0) {
            this.lbsService.updateBackupConfig(this.backupConfig)
                .then(function () { return _this.goBack(); });
        }
        else {
            this.lbsService.createBackupConfig(this.backupConfig)
                .then(function () { return _this.goBack(); });
        }
    };
    BackupConfigComponent.prototype.goBack = function () {
        this.location.back();
    };
    BackupConfigComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'backup-config',
            templateUrl: 'backup-config.component.html',
            styleUrls: ['backup-config.component.css']
        }), 
        __metadata('design:paramtypes', [lbs_service_1.LbsService, router_1.ActivatedRoute, common_1.Location])
    ], BackupConfigComponent);
    return BackupConfigComponent;
}());
exports.BackupConfigComponent = BackupConfigComponent;
/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/ 
//# sourceMappingURL=backup-config.component.js.map