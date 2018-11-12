module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        
        clean: {
            Content: [
            'gellmvc/Content/**/*',
            '!gellmvc/Content/images/**/*',
            '!gellmvc/Content/images'
            ]
        },

        // Compile SASS files into minified CSS.
        sass: {
            options: {
                // bootstrap SCSS files to use with @import.
                includePaths: ['bower_components/bootstrap-sass/assets/stylesheets']
            },
            dist: {
                options: {
                    //outputStyle: 'compressed'
                },
                // the file that i want to compile. eg, 'dist/compiled.css' : 'scss/source.scss'
                files: {
                    'gellmvc/Content/site.css': 'gellmvc/scss/site.scss'
                }
            }
        },

        // Copy web assets from bower_components to more convenient directories.
        copy: {
            main: {
                files: [
                    ////////////////////// Vendor scripts.

                    //bootstrap-sass
                    {
                        expand: true,
                        cwd: 'bower_components/bootstrap-sass/assets/javascripts/',
                        src: ['**/*.js'],
                        dest: 'gellmvc/Content/bootstrap-sass/'
                    },

                    //jquery
                    {
                        expand: true,
                        cwd: 'bower_components/jquery/dist/',
                        src: ['**/*.js', '**/*.map'],
                        dest: 'gellmvc/Content/jquery/'
                    },

                    //jquery.validation
                    {
                        expand: true,
                        cwd: 'bower_components/jquery.validation/dist/',
                        src: ['**/*.js'],
                        dest: 'gellmvc/Content/jquery.validation/'
                    },

                    // Microsoft.jQuery.Unobtrusive.Validation
                    {
                        expand: true,
                        cwd: 'bower_components/Microsoft.jQuery.Unobtrusive.Validation/',
                        src: ['**/*.js'],
                        dest: 'gellmvc/Content/Microsoft.jQuery.Unobtrusive.Validation/'
                    },

                    // Fonts.
                    {
                        expand: true,
                        filter: 'isFile',
                        flatten: true,
                        cwd: 'bower_components/',
                        src: ['bootstrap-sass/assets/fonts/**'],
                        dest: 'gellmvc/Content/fonts/bootstrap/'
                    },

                    // site images.
                    //{
                    //    expand: true,
                    //    src: ['site_images/**'],
                    //    dest: 'gellmvc/Content/'
                    //},
                ]
            },
        },

        // Watch these files and notify of changes.
        //watch: {
        //    grunt: { files: ['Gruntfile.js'] },
        //
        //    sass: {
        //        files: [
        //            'gellmvc/scss/**/*.scss'
        //        ],
        //        tasks: ['sass']
        //    }
        //}
    });

    // Load externally defined tasks. 
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-sass'); // maybe should be using grunt-contrib-sass
    //grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');


    // Establish tasks we can run from the terminal.
    grunt.registerTask('default', ['clean', 'copy', 'sass']); // must copy first. so that sass will be able to build
    //grunt.registerTask('build', ['clean', 'copy', 'sass']);
    //grunt.registerTask('default', ['clean', 'build', 'watch']);
}