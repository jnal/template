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
var http_1 = require('@angular/http');
require('rxjs/add/operator/toPromise');
var machine_1 = require('./machine');
var LbsService = (function () {
    function LbsService(http) {
        this.http = http;
        this.headers = new http_1.Headers({ 'Content-Type': 'application/json' });
        this.machineUrl = 'http://localhost:1673/api/machine'; // URL to web api
        //private backupConfigUrl = 'app/backupConfigs';
        this.backupLogUrl = 'http://localhost:1673/api/Log';
    }
    LbsService.prototype.getMachines = function () {
        return this.http.get(this.machineUrl)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    //getBackupConfigs(): Promise<BackupConfig[]> {
    //    return this.http.get(this.backupConfigUrl)
    //        .toPromise()
    //        .then(response => response.json().data as BackupConfig[])
    //        .catch(this.handleError);
    //}
    //getAllLogs(): Promise<BackupLog[]> {
    //    return this.http.get(this.backupLogUrl)
    //        .toPromise()
    //        .then(response => response.json().data as BackupLog[])
    //        .catch(this.handleError);
    //}
    LbsService.prototype.getLogs = function (backupConfigId) {
        var url = this.backupLogUrl + "/machinebackup/" + backupConfigId;
        return this.http
            .get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    //getMachine(id: number): Promise<Machine> {
    //  return this.getMachines()
    //      .then(machines => machines.find(machine => machine.id === id));
    //}
    LbsService.prototype.getBackupConfig = function (id) {
        var url = this.machineUrl + "/backup/" + id;
        return this.http
            .get(url)
            .toPromise()
            .then(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    LbsService.prototype.delete = function (id) {
        var url = this.machineUrl + "/" + id;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(function () { return null; })
            .catch(this.handleError);
    };
    LbsService.prototype.deleteBackupConfig = function (id) {
        var url = this.machineUrl + "/backup/" + id;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(function () { return null; })
            .catch(this.handleError);
    };
    LbsService.prototype.enableDisableMachine = function (id, isActive) {
        var url = this.machineUrl + "/enabledisable/" + id + "/" + isActive;
        return this.http
            .put(url, null)
            .toPromise()
            .then(function () { return isActive; })
            .catch(this.handleError);
    };
    LbsService.prototype.create = function (ipAddress) {
        var machine = new machine_1.Machine();
        machine.ipAddress = ipAddress;
        machine.isActive = true;
        return this.http
            .post(this.machineUrl, JSON.stringify(machine), { headers: this.headers })
            .toPromise()
            .then(function (res) { return res.json(); })
            .catch(this.handleError);
    };
    LbsService.prototype.update = function (machine) {
        var url = this.machineUrl + "/" + machine.id;
        return this.http
            .put(url, JSON.stringify(machine), { headers: this.headers })
            .toPromise()
            .then(function () { return machine; })
            .catch(this.handleError);
    };
    LbsService.prototype.createBackupConfig = function (backupConfig) {
        var url = this.machineUrl + "/backup";
        return this.http
            .post(url, JSON.stringify(backupConfig), { headers: this.headers })
            .toPromise()
            .then(function () { return backupConfig; })
            .catch(this.handleError);
    };
    LbsService.prototype.updateBackupConfig = function (backupConfig) {
        var url = this.machineUrl + "/backup";
        return this.http
            .put(url, JSON.stringify(backupConfig), { headers: this.headers })
            .toPromise()
            .then(function () { return backupConfig; })
            .catch(this.handleError);
    };
    LbsService.prototype.handleError = function (error) {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    };
    LbsService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], LbsService);
    return LbsService;
}());
exports.LbsService = LbsService;
/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/ 
//# sourceMappingURL=lbs.service.js.map