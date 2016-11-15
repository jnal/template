import { Component, OnInit }      from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location }               from '@angular/common';

import { BackupConfig }         from './backup-config';
import { LbsService }           from './lbs.service';

@Component({
  moduleId: module.id,
  selector: 'backup-config',
  templateUrl: 'backup-config.component.html',
  styleUrls: [ 'backup-config.component.css' ]
})
export class BackupConfigComponent implements OnInit {
    backupConfig: BackupConfig;

  constructor(
      private lbsService: LbsService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
        let id = +params['id'];

        let method = params['method'];

        if (method === 'update' && id > 0) {
            this.lbsService.getBackupConfig(id)
                .then(backupConfig => this.backupConfig = backupConfig);
        } else {
            let backupConfig = new BackupConfig();
            backupConfig.schedule = new Date();
            backupConfig.machineId = id;
            this.backupConfig = backupConfig;

        }
    });
  }

  save(): void {
      if (this.backupConfig.id > 0) {
          this.lbsService.updateBackupConfig(this.backupConfig)
              .then(() => this.goBack());
      } else {
          this.lbsService.createBackupConfig(this.backupConfig)
              .then(() => this.goBack());
      }
  }

  goBack(): void {
    this.location.back();
  }
}


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/