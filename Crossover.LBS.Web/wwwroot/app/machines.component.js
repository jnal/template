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
var lbs_service_1 = require('./lbs.service');
var MachinesComponent = (function () {
    function MachinesComponent(lbsService, router) {
        this.lbsService = lbsService;
        this.router = router;
    }
    MachinesComponent.prototype.getMachines = function () {
        var _this = this;
        this.lbsService
            .getMachines()
            .then(function (machines) {
            _this.machines = machines;
        });
    };
    MachinesComponent.prototype.add = function (ipAddress) {
        var _this = this;
        ipAddress = ipAddress.trim();
        if (!ipAddress) {
            return;
        }
        this.lbsService.create(ipAddress)
            .then(function (machine) {
            _this.machines.push(machine);
            _this.selectedMachine = null;
        });
    };
    MachinesComponent.prototype.enableDisable = function (machine) {
        var _this = this;
        this.lbsService
            .enableDisableMachine(machine.id, !machine.isActive)
            .then(function (b) {
            _this.machines.find(function (m) { return m === machine; }).isActive = b;
        });
    };
    MachinesComponent.prototype.delete = function (machine) {
        var _this = this;
        this.lbsService
            .delete(machine.id)
            .then(function () {
            _this.machines = _this.machines.filter(function (h) { return h !== machine; });
            if (_this.selectedMachine === machine) {
                _this.selectedMachine = null;
            }
        });
    };
    MachinesComponent.prototype.deleteBackupConfig = function (backupConfig) {
        var _this = this;
        this.lbsService
            .delete(backupConfig.id)
            .then(function () {
            _this.selectedMachine.backupConfigs = _this.selectedMachine.backupConfigs.filter(function (b) { return b !== backupConfig; });
        });
    };
    MachinesComponent.prototype.ngOnInit = function () {
        this.getMachines();
    };
    MachinesComponent.prototype.onSelect = function (machine) {
        this.selectedMachine = machine;
    };
    MachinesComponent.prototype.gotoAddBackupConfig = function () {
        this.router.navigate(['/backupconfig/add', this.selectedMachine.id]);
    };
    MachinesComponent.prototype.gotoEditBackupConfig = function (id) {
        this.router.navigate(['/backupconfig/update', id]);
    };
    MachinesComponent.prototype.gotoLogs = function (id) {
        this.router.navigate(['/logs', id]);
    };
    MachinesComponent.prototype.gotoDetail = function () {
        this.router.navigate(['/detail', this.selectedMachine.id]);
    };
    MachinesComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: 'machines',
            templateUrl: 'machines.component.html',
            styleUrls: ['machines.component.css']
        }), 
        __metadata('design:paramtypes', [lbs_service_1.LbsService, router_1.Router])
    ], MachinesComponent);
    return MachinesComponent;
}());
exports.MachinesComponent = MachinesComponent;
/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/ 
//# sourceMappingURL=machines.component.js.map