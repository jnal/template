"use strict";
var testing_1 = require('@angular/core/testing');
var platform_browser_1 = require('@angular/platform-browser');
var backup_config_component_1 = require('./backup-config.component');
var lbs_service_1 = require('./lbs.service');
var common_1 = require('@angular/common');
var router_1 = require('@angular/router');
describe('BackupConfigComponent', function () {
    var comp;
    var fixture;
    var componentUserService; // the actually injected service
    var lbsService; // the TestBed injected service
    var de; // the DebugElement with the welcome message
    var el; // the DOM element with the welcome message
    var lbsServiceStub;
    var activatedRouteStub;
    var locationStub;
    beforeEach(function () {
        // stub UserService for test purposes
        testing_1.TestBed.configureTestingModule({
            declarations: [backup_config_component_1.BackupConfigComponent],
            // providers:    [ UserService ]  // NO! Don't provide the real service!
            // Provide a test-double instead
            providers: [{ provide: lbs_service_1.LbsService, useValue: lbsServiceStub }, { provide: router_1.ActivatedRoute, useValue: activatedRouteStub }, { provide: common_1.Location, useValue: locationStub }]
        }).compileComponents().then(function () {
            fixture = testing_1.TestBed.createComponent(backup_config_component_1.BackupConfigComponent);
            fixture.detectChanges();
            comp = fixture.componentInstance;
            lbsService = fixture.debugElement.injector.get(lbs_service_1.LbsService);
            componentUserService = lbsService;
            lbsService = testing_1.TestBed.get(lbs_service_1.LbsService);
            //  get the "welcome" element by CSS selector (e.g., by class name)
            de = fixture.debugElement.query(platform_browser_1.By.all());
            el = de.nativeElement;
        });
    });
    it('test', function () {
        var content = el.textContent;
        expect(content).toContain('Welcome', '"Welcome ..."');
    });
});
//# sourceMappingURL=backup-config.component.spec.js.map