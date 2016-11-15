import { DebugElement } from '@angular/core';
import { ComponentFixture, inject,TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

import { BackupConfigComponent } from './backup-config.component';
import { LbsService } from './lbs.service';
import { Location } from '@angular/common';
import { ActivatedRoute, Params } from '@angular/router';



describe('BackupConfigComponent', () => {

    let comp: BackupConfigComponent;
    let fixture: ComponentFixture<BackupConfigComponent>;
    let componentUserService: LbsService; // the actually injected service
    let lbsService: LbsService; // the TestBed injected service
    let de: DebugElement;  // the DebugElement with the welcome message
    let el: HTMLElement; // the DOM element with the welcome message

    let lbsServiceStub: {};
    let activatedRouteStub: {};
    let locationStub: {}

    beforeEach(() => {
        // stub UserService for test purposes
        TestBed.configureTestingModule({
            declarations: [BackupConfigComponent],
            // providers:    [ UserService ]  // NO! Don't provide the real service!
            // Provide a test-double instead
            providers: [{ provide: LbsService, useValue: lbsServiceStub }, { provide: ActivatedRoute, useValue: activatedRouteStub }, { provide: Location, useValue: locationStub }]
        }).compileComponents().then(() => {

            fixture = TestBed.createComponent(BackupConfigComponent);
            fixture.detectChanges();
        comp = fixture.componentInstance;

        lbsService = fixture.debugElement.injector.get(LbsService);
        componentUserService = lbsService;
        lbsService = TestBed.get(LbsService);

        //  get the "welcome" element by CSS selector (e.g., by class name)
        de = fixture.debugElement.query(By.all());
        el = de.nativeElement;
            });
    });

    it('test', () => {
 
        const content = el.textContent;
        expect(content).toContain('Welcome', '"Welcome ..."');
    });
});

