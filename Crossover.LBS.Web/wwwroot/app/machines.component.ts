import { Component, OnInit } from '@angular/core';
import { Router }            from '@angular/router';

import { Machine }                from './machine';
import { LbsService } from './lbs.service';
import { BackupConfig } from './backup-config';

import { Observable } from 'rxjs/observable';

@Component({
  moduleId: module.id,
  selector: 'machines',
  templateUrl: 'machines.component.html',
  styleUrls: [ 'machines.component.css' ]
})
export class MachinesComponent implements OnInit {
  machines: Machine[];
  selectedMachine: Machine;

  constructor(
    private lbsService: LbsService,
    private router: Router) { }

  getMachines(): void {
      this.lbsService
        .getMachines()
          .then(machines => {
              this.machines = machines;
          });
  }

  add(ipAddress: string): void {
      ipAddress = ipAddress.trim();
      if (!ipAddress) { return; }
    this.lbsService.create(ipAddress)
      .then(machine => {
          this.machines.push(machine);
          this.selectedMachine = null;
      });
  }

  enableDisable(machine: Machine): void {
      this.lbsService
          .enableDisableMachine(machine.id, !machine.isActive)
          .then(b => {
              this.machines.find(m => m === machine).isActive = b;
          });
  }

  delete(machine: Machine): void {
      this.lbsService
          .delete(machine.id)
        .then(() => {
            this.machines = this.machines.filter(h => h !== machine);
            if (this.selectedMachine === machine) { this.selectedMachine = null; }
        });
  }


  deleteBackupConfig(backupConfig: BackupConfig): void {
      this.lbsService
          .delete(backupConfig.id)
          .then(() => {
              this.selectedMachine.backupConfigs = this.selectedMachine.backupConfigs.filter(b => b !== backupConfig);
          });
  }

  ngOnInit(): void {
      this.getMachines();
  }

  onSelect(machine: Machine): void {
      this.selectedMachine = machine;
  }


  gotoAddBackupConfig(): void {
      this.router.navigate(['/backupconfig/add', this.selectedMachine.id]);
  }

  gotoEditBackupConfig(id: number): void {
      this.router.navigate(['/backupconfig/update', id]);
  }

  gotoLogs(id: number): void {
      this.router.navigate(['/logs', id]);
  }

  gotoDetail(): void {
      this.router.navigate(['/detail', this.selectedMachine.id]);
  }

}


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/