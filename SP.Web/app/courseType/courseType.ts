﻿import angular from 'angular';
import breezeEnums from './../breezeEnums';

    var controllerId = 'courseType';
export default angular
        .module('app')
        .controller(controllerId, controller).name;

    (controller as any).$inject = ['controller.abstract', '$routeParams', 'common', 'datacontext', '$scope', 'breeze', '$aside', '$q', 'selectOptionMaps', 'loginFactory', '$modal', '$location'];

    function controller(abstractController, $routeParams, common, datacontext, $scope, breeze, $aside, $q, selectOptionMaps, loginFactory, $modal, $location) {
        /* jshint validthis:true */
        var vm = this;
        abstractController.constructor.call(this, {
            controllerId: controllerId,
            watchedEntityNames: ['courseType', 'courseType.courseFormats', 'courseType.courseFormats.courseSlots', 'courseType.courseFormats.courseSlots.activity', 'courseType.courseTypeDepartments', 'courseType.candidatePrereadings'],
            $scope: $scope
        });
        var id = $routeParams.id;
        var isNew = id === 'new';

        //if multiselect=true, vm.activeFormatIndex will become an array, and will
        //note also this variable is used in the functions below to identify which object is updated 
        vm.activeFormatIndex = -1;
        vm.activitySelected = activitySelected;
        vm.addPrereading = addPrereading;
        vm.alterDayMarkers = alterDayMarkers;
        vm.alterObsoleteFormat = alterObsoleteFormat;
        vm.clearFileData = clearFileData;
        vm.clone = clone;
        vm.courseType = {};
        vm.createSlot = createSlot;
        vm.createNewFormat = createNewFormat;
        vm.deleteFormat = deleteFormat;
        vm.departmentRemoved = common.removeCollectionItem.bind(null, datacontext.courseTypeDepartments, courseTypeDeparmentKey);
        vm.departmentSelected = common.addCollectionItem.bind(null, datacontext.courseTypeDepartments, courseTypeDeparmentKey);
        vm.departments = [];
        vm.downloadReadings = downloadReadings;
        vm.downloadCertificate = downloadCertificate;
        vm.editSlot = editSlot;
        vm.editChoices = editChoices;
        vm.emersionCategories = breezeEnums.emersion;
        vm.getCourseActivityNames = getCourseActivityNames;
        vm.instructorCourses = [];
        vm.isScenarioChanged = isScenarioChanged;
        vm.isNoReadingsOnServer = vm.isReadingsOnServer;
        vm.removeSlot = removeSlot;
        vm.resetExampleTimes = resetExampleTimes;

        vm.selectedDepartmentIds = [];
        vm.submit = submit;
        vm.title = 'Course Format';

        vm.sortableOptions = {
            handle: '.handle',
            stop: function () {
                // this callback has the changed model
                var format = vm.courseType.courseFormats[vm.activeFormatIndex];
                var nextSlot = -1;
                //console.log(format.sortableSlots.map(el=> ({ day:el.day, activity:el.activity?el.activity.name:el.isDayMarker?'day':'sim' })));
                format.sortableSlots.forEach(function (cs) {
                    if (!cs.isDayMarker && cs.order !== ++nextSlot) {
                        cs.order = nextSlot;
                    }
                });
                alterDayMarkers(format);
            },
            items: 'tr:not(.not-sortable)'
        };

        activate();

        function activate() {
            datacontext.ready().then(function () {
                var promises = [
                        datacontext.courseTypes.find({ where: breeze.Predicate.create('instructorCourseId', '==', null).and('id', '!=', id) }).then(function (data) {
                            vm.instructorCourses = data;
                        }),
                        datacontext.departments.all({ expand: 'institution.culture' }).then(function (data) {
                            vm.departments = selectOptionMaps.sortAndMapDepartment(data);
                        })];
                if (isNew) {
                    vm.courseType = datacontext.courseTypes.create();
                    var cf = datacontext.courseFormats.create({ courseType: vm.courseType });
                    cf.sortableSlots = [];
                } else {
                    //promises.push(datacontext.courseTypes.fetchByKey(id, { expand: 'courseFormats.courseSlots' }).then(function (data) { - if the courseFormats were not already loaded from the server
                    promises.push(datacontext.courseTypes.fetchByKey(id,
                    {
                        expand: ["courseFormats.courseSlots.activity", "courseTypeDepartments"]
                    }).then(function (data) {
                        if (!data) {
                            vm.log.warning('Could not find courseType id = ' + id);
                            $location.path('/courseTypes');
                            return;
                        }
                        vm.courseType = data;
                        vm.selectedDepartmentIds = vm.courseType.courseTypeDepartments.map(function (ctd) {
                            return ctd.departmentId;
                        });
                        vm.courseType.courseFormats.forEach(function (cf) {
                            resetExampleTimes(cf);
                            cf.sortableSlots = createCourseSlotSortableArray(cf.courseSlots);
                        });
                        vm.activeFormatIndex = vm.courseType.courseFormats.findIndex(function (cf) {
                            return cf.id === $routeParams.formatId;
                        });

                    }));
                }
                common.activateController(promises, controllerId)
                    .then(function () {
                        vm.notifyViewModelLoaded();
                        
                        if (vm.courseType.courseFormats.length === 1) {
                            vm.activeFormatIndex = 0;
                        }
                        vm.log('Activated Course Format View');
                    });
            });
        }
        
        //logic is - can't delete slots after course has been run, as they may have associated tables such as participants
        //-altering the name of a slot - don't allow reassignment of activity, as course participants etc will be all mucked up
        //-only show typeahead if a new slot (otherwise simple input)

        //-if a new slot/activity and activity selected, delete new activity, replace with selected
        function activitySelected(activityName, slot) {
            removeActivity(slot);
            slot.activity = vm.courseType.courseActivities.find(function (el) {
                return el.name === activityName;
            });
            reinstateInactive(slot.courseFormat);
        }

        function addPrereading() {
            datacontext.candidatePrereadings.create({ courseTypeId: vm.courseType.id });
        }

        function clearFileData() {
            vm.courseType.certificateFileName = vm.courseType.fileSize = vm.courseType.fileModified = vm.courseType.file = null;
        }

        function createActivity(slot) {
            slot.activity = datacontext.courseActivities.create({
                courseTypeId: vm.courseType.id
            });
        }

        function courseTypeDeparmentKey(departmentId){
            return {
                departmentId: departmentId,
                courseTypeId: vm.courseType.id
            };
        }

        //we have fiddled with the entity model rather than create a view model (naughty/lazy) - tear down those mods here
        /* now being handled (rightly or wrongly - quite specific for current BreezeJs implementation) by controller.abstract 
        function destroy() {
            if (vm.courseType) {
                vm.courseType.courseFormats.forEach(function (cf) {
                    delete cf.selectedSlot;
                    delete cf.sortableSlots;
                    delete cf.defaultStartTime;
                    cf.courseSlots.forEach(function (cs) {
                        delete cs.exampleFinish;
                    });
                });
            }
        }

        function removeActivity(slot) {
            var ca = slot.activity;
            if (ca && ca.entityAspect.entityState.isAdded()) {
                ca.entityAspect.setDeleted();
            }
            slot.activityId = slot.activity = null;
        }
        */
        function downloadReadings() {
            loginFactory.downloadFileLink('CandidateReading', vm.courseType.id)
                .then(function (url) {
                    vm.downloadFileUrl = url;
                });
        }

        function downloadCertificate() {
            loginFactory.downloadFileLink('CertificateTemplate', vm.courseType.id)
                .then(function (url) {
                    vm.downloadFileUrl = url;
                });
        }

        function getCourseActivityNames(name) {
            name = name.toLowerCase();
            var returnVar =[];
            vm.courseType.courseActivities.forEach(function (el) {
                if (el.name.toLowerCase().indexOf(name) !== -1) {
                    returnVar.push(el.name);
                }
            });
            return returnVar;
        }
        
        /*
        function saveOverride() {
            //vm.log.debug($event);
            baseSave().then(destroy);
        }//;
        */

        function createSlot(courseFormat) {
            courseFormat.selectedSlot = datacontext.courseSlots.create({
                courseFormatId: courseFormat.id,
                order: (courseFormat.courseSlots || []).length,
                day: courseFormat.daysDuration
            });
            courseFormat.sortableSlots.push(courseFormat.selectedSlot);
            createActivity(courseFormat.selectedSlot);
        }

        function removeSlot(courseSlot) {
            var format = courseSlot.courseFormat;
            var sortableSlots = format.sortableSlots;
            var indx = sortableSlots.indexOf(courseSlot);

            removeActivityAndSlots(courseSlot);
            sortableSlots.splice(indx, 1);
            //delete courseSlot.courseFormat.selectedSlot; - not necessarily selected
            resetExampleTimes(format);
        }

        function editChoices(slot) {
            var modal = getModalInstance();
            modal.$scope.courseActivity = slot.activity;
            modal.$promise.then(modal.show);
        }

        function editSlot(courseSlot) {
            courseSlot.courseFormat.selectedSlot = courseSlot;
        }

        function isScenarioChanged(slot) {
            //whatever it is (e.g. isScenario) we will change to the opposite by adding or removing activity
            if (slot.isScenario) {
                createActivity(slot);
            } else {
                removeActivity(slot);
                reinstateInactive(slot.courseFormat);
            }
        }

        //if a courseSlot exists, but is marked inactive
        //AND the courseSlot has the same courseActivity as the one just chosen
        //(or in the case of a scenario the first scenario slot marked inactive)
        //mark the existing (inactive) slot as active, make this the selected slot
        //and delete the new slot before being persisted to the db
        //this is so participants and faculty can be tracked more easily 
        //
        //
        function reinstateInactive(courseFormat) {
            var selectedSlot = courseFormat.selectedSlot;
            if (!selectedSlot.entityAspect.entityState.isAdded()) {
                return;
            }
            var existingSlot;
            if (selectedSlot.isScenario) {
                existingSlot = courseFormat.courseSlots.find(function (el) {
                    return !el.isActive && el !== selectedSlot && el.isScenario;
                });
            } else {
                existingSlot = courseFormat.courseSlots.find(function (el) {
                    return !el.isActive && el !== selectedSlot && el.activity === selectedSlot.activity;
                });
            }
            if (existingSlot) { 
                selectedSlot.entityAspect.setDeleted();
                courseFormat.selectedSlot = existingSlot;
                existingSlot.isActive = true;
                existingSlot.order = selectedSlot.order;
                var indx = courseFormat.sortableSlots.indexOf(selectedSlot);
                courseFormat.sortableSlots[indx] = existingSlot;
            }
        }

        var _modalInstance;
        function getModalInstance() {
            if (!_modalInstance) {
                var scope = $scope.$new();
                _modalInstance = $aside({
                        templateUrl: 'app/activityResources/activityResource.html',
                        controller: 'activityResource',
                    show: false,
                    id: 'cpModal',
                    placement: 'left',
                        animation: 'am-slide-left',
                    scope: scope,
                    controllerAs: 'ar'
                });
            }
            return _modalInstance;
        }

        function clone(cf) {
            var newFormat = datacontext.cloneItem(cf, ['courseSlots']);
            newFormat.description += " - copy";
            newFormat.sortableSlots = createCourseSlotSortableArray(newFormat.courseSlots);
            vm.activeFormatIndex = vm.courseType.courseFormats.length -1;
        }

        function createNewFormat() {
            var cf = datacontext.courseFormats.create({ courseTypeId: vm.courseType.id });
            cf.sortableSlots = createCourseSlotSortableArray(cf.courseSlots);
            vm.activeFormatIndex = vm.courseType.courseFormats.length - 1;
        }

        function deleteFormat(cf) {
            cf.courseSlots.forEach(function (el) {
                el.entityAspect.setDeleted();
            });
            cf.entityAspect.setDeleted();
        }

        function resetExampleTimes(cf) {
            var currentDay;
            var startIndx;
            
            cf.courseSlots.sort(common.sortOnPropertyName('order'));
            cf.courseSlots.forEach(function (cs) {
                if (cs.isActive) {
                    if (cs.day !== currentDay) {
                        startIndx = cf.defaultStartAsDate;
                        currentDay = cs.day;
                    }
                    startIndx += cs.minutesDuration * 60000;
                    cs.exampleFinish = new Date(startIndx);
                }
            });
        }

        function createCourseSlotSortableArray(slots) {
            var returnVar = [];
            var currentDay = -1;

            slots.forEach(function (cs) {
                if (cs.isActive) {
                    if (currentDay !== cs.day) {
                        returnVar.push({ isDayMarker: true, day: cs.day, locked: cs.day === 1, isActive: true });
                        currentDay = cs.day;
                    }
                    returnVar.push(cs);
                }
            });
            return returnVar;
        }

        vm.isNoReadingsOnServer = function () {
            return !(vm.courseType.candidatePrereadings && vm.courseType.candidatePrereadings.some(function (el) { return el.entityAspect.entityState.isUnchanged(); }));
        };

        function alterDayMarkers(cf) {
            if (cf.daysDuration <= 0) { return; }
            var currentDay = 0;
            var i;
            for (i = 0; i < cf.sortableSlots.length; i++) {
                if (cf.sortableSlots[i].isDayMarker && ++currentDay > cf.daysDuration) {
                    cf.sortableSlots.splice(i, 1);
                    --currentDay;
                    --i;
                } else {
                    cf.sortableSlots[i].day = currentDay;
                }
            }
            for (i = currentDay; i < cf.daysDuration; i++) {
                cf.sortableSlots.push({ isDayMarker: true, day: i+1, locked: false, isActive:true });
            }
            resetExampleTimes(cf);
        }

        

        function alterObsoleteFormat(cf) {
            if (cf.obsolete) {
                var forDelete;
                var modal;
                var options = {
                    canDeleteType: vm.courseType.courseFormats.length <= 1,
                    deleteFormat: function() {
                        forDelete.forEach(deleteCollection);

                    },
                    deleteType: function () {
                        vm.courseType.courseTypeDepartments.forEach(deleteCollection);
                        forDelete.forEach(deleteCollection);
                        vm.courseType.entityAspect.setDeleted();
                    }
                };
                modal = $modal({
                    templateUrl: '/app/courseType/deleteCourseType.tpl.html',
                    show: false,
                    controller: 'deleteCourseTypeCtrl',
                    resolve: { options: function () { return options; } }
                });
                $q.all([deletableFormat(cf).then(function (data) {
                    forDelete = data;
                }), modal.$promise]).then(function () {
                    if (forDelete.length) {
                        modal.show();
                    }
                });
            }
            function deleteCollection(el) {
                el.entityAspect.setDeleted();
            }
        }

        function deletableFormat(cf) {
            return cf.entityAspect.loadNavigationProperty('courses').then(function (data) {
                if (data.results.length) {
                    return [];
                } else {
                    var returnVar = [cf].concat(cf.courseSlots);
                    //assuming course slots are loaded as they are an integral part of the page
                    //to be ultra safe I guess could do a $q.all datacontext.ready 
                    cf.courseSlots.forEach(function (cs) {
                        returnVar.push(cs.activity);
                        returnVar = returnVar.concat(
                            cs.activity.activityChoices.filter(function (ac) {
                                return !ac.courseActivity.courseSlots.some(function (e) {
                                    return e.courseFormat !== cf;
                                });
                            }));
                    });

                    return returnVar;
                }
            });
        }

        function canDeleteSlot(cs) {
            if (cs.entityAspect.entityState.isAdded()) {
                return $q.when(true);
            }
            var navPropsToCheck = cs.isScenario
                ? ["courseScenarioFacultyRoles", "courseSlotActivities", "courseSlotManikins"]
                : ["courseSlotPresenters", "courseSlotActivities"];
            if (!forDeletion()) {
                return $q.when(false);
            }
            var navPropsToLoad = navPropsToCheck.filter(function (p) {
                return !cs[p].isNavigationPropertyLoaded;
            });
            if (navPropsToLoad.length) {
                return datacontext.courseSlots.fetchByKey(cs.id, { expand: navPropsToLoad })
                    .then(function () { return forDeletion(); });
            } else {
                return $q.when(forDeletion());
            }

            function forDeletion() {
                return navPropsToCheck.every(function (p) {
                    return !cs[p].length;
                });
            }
        }

        function deleteSlot(cs) {
            return canDeleteSlot(cs).then(function (canDel) {
                if (canDel) {
                    cs.entityAspect.setDeleted();
                } else {
                    cs.isActive = false;
                }
            });

        }

        function removeActivity(cs) {
            if (cs.activity) {
                var slotsSharingActivity = cs.activity.courseSlots.filter(function (slotSharingActivity) {
                    return slotSharingActivity.id !== cs.id;
                });
                if (slotsSharingActivity.length) {
                    if (slotsSharingActivity.every(function (slotSharingActivity) {
                        return !slotSharingActivity.isActive;
                    })) {
                        cs.activity.entityAspect.setUnchanged();
                    }
                } else {
                    cs.activity.entityAspect.setDeleted();
                }
            }
            cs.activityId = cs.activity = null;
        }

        function removeActivityAndSlots(cs) {
            if (cs.isScenario) {
                deleteSlot(cs);
            } else if(cs.activity.courseSlots.every(function (slotsSharingActivity) {
                return slotsSharingActivity.id === cs.id || !slotsSharingActivity.isActive;
            })) {
                return $q.all(cs.activity.courseSlots.map(function (slotsSharingActivity) {
                    return deleteSlot(slotsSharingActivity); //should do an in here using Breeze Json notation
                })).then(function () {
                    removeActivity(cs);
                });
            } else {
                cs.isActive = false;
            }
        }

        function submit(){
            vm.save().then(function () {
                if (vm.courseType.entityAspect.entityState.isDetached()) {
                    $location.path('/courseTypes');
                }
            });
        }
    }

