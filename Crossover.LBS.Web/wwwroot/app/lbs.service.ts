import { Injectable }    from '@angular/core';
import { Headers, Http, Response, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Machine } from './machine';
import { BackupConfig } from './backup-config';
import { BackupLog } from './backup-log'

@Injectable()
export class LbsService {

    private headers = new Headers({ 'Content-Type': 'application/json'});
  private machineUrl = 'http://localhost:1673/api/machine';  // URL to web api
  //private backupConfigUrl = 'app/backupConfigs';
  private backupLogUrl = 'http://localhost:1673/api/Log';

  constructor(private http: Http) { }

  getMachines(): Promise<Machine[]> {

      return this.http.get(this.machineUrl)
               .toPromise()
               .then(response => response.json() as Machine[])
          .catch(this.handleError);

  }

    
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

  getLogs(backupConfigId: number): Promise<BackupLog[]> {

      const url = `${this.backupLogUrl}/machinebackup/${backupConfigId}`;
      return this.http
          .get(url)
          .toPromise()
          .then(response => response.json() as BackupLog[])
          .catch(this.handleError);
  }

  //getMachine(id: number): Promise<Machine> {
  //  return this.getMachines()
  //      .then(machines => machines.find(machine => machine.id === id));
  //}

  getBackupConfig(id: number): Promise<BackupConfig> {
      const url = `${this.machineUrl}/backup/${id}`;
      return this.http
          .get(url)
          .toPromise()
          .then(response => response.json() as BackupConfig)
          .catch(this.handleError);
  }

  delete(id: number): Promise<void> {
      const url = `${this.machineUrl}/${id}`;
    return this.http.delete(url, {headers: this.headers})
      .toPromise()
      .then(() => null)
      .catch(this.handleError);
  }


  deleteBackupConfig(id: number): Promise<void> {
      const url = `${this.machineUrl}/backup/${id}`;
      return this.http.delete(url, { headers: this.headers })
          .toPromise()
          .then(() => null)
          .catch(this.handleError);
  }

  enableDisableMachine(id: number, isActive: boolean): Promise<boolean> {
      const url = `${this.machineUrl}/enabledisable/${id}/${isActive}`;
      return this.http
          .put(url, null)
          .toPromise()
          .then(() => isActive)
          .catch(this.handleError);
  }

  create(ipAddress: string): Promise<Machine> {

      let machine = new Machine();
      machine.ipAddress = ipAddress;
      machine.isActive = true;

    return this.http
        .post(this.machineUrl, JSON.stringify(machine), {headers: this.headers})
      .toPromise()
      .then(res => res.json())
      .catch(this.handleError);
  }

  update(machine: Machine): Promise<Machine> {
      const url = `${this.machineUrl}/${machine.id}`;
    return this.http
        .put(url, JSON.stringify(machine), {headers: this.headers})
      .toPromise()
        .then(() => machine)
      .catch(this.handleError);
  }


  createBackupConfig(backupConfig: BackupConfig): Promise<BackupConfig> {
      const url = `${this.machineUrl}/backup`;
      return this.http
          .post(url, JSON.stringify(backupConfig), { headers: this.headers })
          .toPromise()
          .then(() => backupConfig)
          .catch(this.handleError);
  }

  updateBackupConfig(backupConfig: BackupConfig): Promise<BackupConfig> {
      const url = `${this.machineUrl}/backup`;
      return this.http
          .put(url, JSON.stringify(backupConfig), { headers: this.headers })
          .toPromise()
          .then(() => backupConfig)
          .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}



/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/