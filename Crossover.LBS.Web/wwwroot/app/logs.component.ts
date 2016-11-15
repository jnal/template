import { Component, OnInit }      from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location }               from '@angular/common';

import { Machine } from './machine';
import { BackupLog } from './backup-log'
import { LbsService } from './lbs.service';

@Component({
  moduleId: module.id,
  selector: 'logs',
  templateUrl: 'logs.component.html',
  styleUrls: [ 'logs.component.css' ]
})
export class LogsComponent implements OnInit {
  logs: BackupLog[];

  constructor(
      private lbsService: LbsService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      let id = +params['id'];
      this.lbsService.getLogs(id)
          .then(logs => {
              this.logs = logs;
          });
    });
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