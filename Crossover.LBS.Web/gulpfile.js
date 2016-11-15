var gulp = require('gulp');
var open = require('gulp-open');
var msbuild = require("gulp-msbuild");

var shell = require('gulp-shell');

var libs = './wwwroot/libs/';

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('restore:core-js', function () {
    gulp.src([
        'node_modules/core-js/client/*.js'
    ]).pipe(gulp.dest(libs + 'core-js'));
});
gulp.task('restore:zone.js', function () {
    gulp.src([
        'node_modules/zone.js/dist/*.js'
    ]).pipe(gulp.dest(libs + 'zone.js'));
});
gulp.task('restore:reflect-metadata', function () {
    gulp.src([
        'node_modules/reflect-metadata/reflect.js'
    ]).pipe(gulp.dest(libs + 'reflect-metadata'));
});
gulp.task('restore:systemjs', function () {
    gulp.src([
        'node_modules/systemjs/dist/*.js'
    ]).pipe(gulp.dest(libs + 'systemjs'));
});
gulp.task('restore:rxjs', function () {
    gulp.src([
        'node_modules/rxjs/**/*.js'
    ]).pipe(gulp.dest(libs + 'rxjs'));
});
gulp.task('restore:angular-in-memory-web-api', function () {
    gulp.src([
        'node_modules/angular-in-memory-web-api/**/*.js'
    ]).pipe(gulp.dest(libs + 'angular-in-memory-web-api'));
});

gulp.task('restore:angular', function () {
    gulp.src([
        'node_modules/@angular/**/*.js'
    ]).pipe(gulp.dest(libs + '@angular'));
});

gulp.task('restore:plugin-typescript', function () {
    gulp.src([
        'node_modules/plugin-typescript/**/*.js'
    ]).pipe(gulp.dest(libs + 'plugin-typescript'));
});

gulp.task('restore:typescript', function () {
    gulp.src([
        'node_modules/typescript/lib/typescript.js'
    ]).pipe(gulp.dest(libs + 'typescript'));
});

gulp.task('restore:ng2-datetime-picker', function () {
    gulp.src([
        'node_modules/ng2-datetime-picker/**/*.js'
    ]).pipe(gulp.dest(libs + 'ng2-datetime-picker'));
});


gulp.task('restore:bootstrap', function () {
    gulp.src([
        'node_modules/bootstrap/dist/**/*.*'
    ]).pipe(gulp.dest(libs + 'bootstrap'));
});

gulp.task('restore', [
    'restore:core-js',
    'restore:zone.js',
    'restore:reflect-metadata',
    'restore:systemjs',
    'restore:rxjs',
    'restore:angular-in-memory-web-api',
    'restore:angular',
    'restore:bootstrap'
]);

gulp.task('jasmine-test', function () {
    gulp.src(__filename)
    .pipe(open({ uri: 'http://localhost:7083/test.html' }));
});

gulp.task('msbuild', () => {
    return gulp.src('Crossover.LBS.Web.xproj')
        .pipe(msbuild({
            properties: { Configuration: 'Release', DeployOnBuild: true, PublishProfile: "local" },
            logCommand: true,
            nologo: false,
            toolsVersion: 'auto'
        })
        );
});



gulp.task('mstest',
    shell.task([
        '"C:\\Program Files\\dotnet\\dotnet.exe" test "%cd%\\..\\Crossover.LBS.API.Tests\\project.json"'
    ]));