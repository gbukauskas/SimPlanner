﻿(function () {
    'use strict';
    var controllerId = 'courseRoles';
    angular
        .module('app')
        .controller(controllerId, controller);

    controller.$inject = ['controller.abstract', '$routeParams', 'common', 'datacontext', '$aside', 'breeze', '$scope'];

    function controller(abstractController, $routeParams, common, datacontext,  $aside, breeze, $scope) {
        /* jshint validthis:true */
        var vm = this;
        abstractController.constructor.call(this, {
            controllerId: controllerId,
            $scope: $scope
        });
        var id = $routeParams.id;
        
        vm.changeScenario = changeScenario;
        vm.course = {};
        vm.scenarios = [];

        vm.title = 'course roles';

        activate();

        function activate() {
            var manequins = [];
            datacontext.ready().then(function () {
                common.activateController([datacontext.courses.fetchByKey(id, {
                    expand: ['courseParticipants.participant',
                        'courseFormat.courseSlots.activity.activityChoices',
                        'courseSlotScenarios',
                        'courseSlotPresenters',
                        'courseSlotManequins',
                        'courseScenarioFacultyRoles',
                        'chosenTeachingResources',
                        'department.scenarios',
                        'courseFormat.courseType.courseTypeScenarioRoles.facultyScenarioRole']
                }).then(function (data) {
                    if (!data) {
                        vm.log.warning('Could not find course id = ' +id);
                        return;
                        //gotoCourses();
                    }
                    var slotTime = data.start;
                    var count = 0;

                    vm.course = data;
                    vm.scenarios = data.department.scenarios;

                    data.courseFormat.courseSlots.sort(common.sortOnPropertyName('order'));
                    data.courseParticipants.sort(common.sortOnChildPropertyName('participant', 'fullName'));

                    //map faculty
                    var faculty = [];

                    data.courseParticipants.forEach(function (el) {
                        if (el.isFaculty) {
                            //add the icon property so we can access it in the other categories
                            faculty.push(angular.extend(calculateCourseRoles(el.participantId),
                                                {
                                                    participantId: el.participantId,
                                                    fullName: el.participant.fullName,
                                                    isConfirmed:el.isConfirmed,
                                                    icon: common.getRoleIcon(el.participant.professionalRole.category),
                                                    departmentName: el.participant.department.abbreviation
                                                }));
                        }
                    });
                    //end region map faculty

                    vm.map = data.courseFormat.courseSlots.map(function(cs){
                        var start = slotTime;
                        var returnVar = {
                            id: cs.id,
                            start: start,
                            groupClass: 'grp' + count++,
                            availableFaculty: faculty.slice()
                        };
                        var sortableOptions = {
                            connectWith: '.' + returnVar.groupClass
                        };
                        returnVar.availableFacultyOptions = angular.extend({
                            update: updateSortable
                        },sortableOptions);
                        var isThisSlot = function (el) { return el.courseSlotId === cs.id; };
                        slotTime = new Date(start.getTime() + cs.minutesDuration * 60000);
                        if (cs.activity) {
                            var assignedFaculty = [];
                            data.courseSlotPresenters.forEach(function (csp) {
                                if (csp.courseSlotId === cs.id) {
                                    var indx = returnVar.availableFaculty.findIndex(function (f) {
                                        return f.participantId === csp.participantId;
                                    });
                                    assignedFaculty.push(returnVar.availableFaculty[indx]);
                                    returnVar.availableFaculty.splice(indx, 1);
                                }
                            });
                            sortableOptions.update = updateSortableRepo.bind(null, {
                                courseSlotId: cs.id,
                                courseId: data.id
                            }, datacontext.courseSlotPresenters);

                            return angular.extend(returnVar, {
                                name: cs.activity.name,
                                start: start,
                                choices: cs.activity.activityChoices,
                                selectedChoice: (data.chosenTeachingResources.find(isThisSlot) || {}).activityTeachingResource,
                                isSim: false,
                                assignedFaculty: assignedFaculty,
                                sortableOptions: sortableOptions
                            });
                        }
                        //extra properties to allow duplication
                        returnVar.availableFacultyOptions.start = startSortable;
                        returnVar.availableFaculty.availableFaculty = true;

                        var slotRoles = cs.courseScenarioFacultyRoles.filter(isThisSlot);
                        data.courseFormat.courseType.courseTypeScenarioRoles.sort(common.sortOnChildPropertyName('facultyScenarioRole', 'order'));
                        angular.extend(returnVar, {
                            name: 'Simulation',
                            scenario: (data.courseSlotScenarios.find(isThisSlot) || {}).scenario,
                            isSim: true,
                            courseSlotManequins: data.courseSlotManequins,
                            roles: data.courseFormat.courseType.courseTypeScenarioRoles.map(function (ctsr) {
                                var assignedFaculty = [];
                                slotRoles.forEach(function (sr) {
                                    if (ctsr.facultyScenarioRoleId === sr.facultyScenarioRoleId) {
                                        var findMatch = function (f) {
                                            return f.participantId === sr.participantId;
                                        };
                                        var indx = returnVar.availableFaculty.findIndex(findMatch);
                                        if (indx === -1) {
                                            assignedFaculty.push(faculty.find(findMatch));
                                        } else {
                                            assignedFaculty.push(returnVar.availableFaculty[indx]);
                                            returnVar.availableFaculty.splice(indx, 1);
                                        }
                                    }
                                });

                                return {
                                    description: ctsr.facultyScenarioRole.description,
                                    id: ctsr.facultyScenarioRoleId,
                                    assignedFaculty: assignedFaculty,
                                    sortableOptions: angular.extend({
                                        update: updateSortableRepo.bind(null, {
                                            courseSlotId: cs.id,
                                            courseId: data.id,
                                            facultyScenarioRoleId: ctsr.facultyScenarioRoleId
                                        }, datacontext.courseScenarioFacultyRoles),
                                        start: startSortable
                                    }, sortableOptions)
                                };
                            })
                        });
                        returnVar.oldScenario = returnVar.scenario;
                        return returnVar;
                    });
                    vm.notifyViewModelLoaded();
                }), datacontext.manequins.all().then(function (data) {
                    var departmentName;
                    data.sort(common.sortOnChildPropertyName('department', 'name'));
                    data.forEach(function (el) {
                        if (departmentName !== el.department.name) {
                            manequins.push({
                                description: departmentName = el.department.name,
                                isGroup:true
                            });
                        }
                        manequins.push(el);
                    });
                })], controllerId).then(function () {
                    vm.map.forEach(function (el, indx) {
                        if (el.courseSlotManequins) {
                            el.manequins = manequins.map(function (m) {
                                return m.isGroup
                                    ? m
                                    : {
                                        checked: el.courseSlotManequins.some(function (cs) {
                                            return cs.manequinId === m.id;
                                        }),
                                        description: m.description,
                                        id: m.id
                                    };
                            });
                            el.selectedManequins = el.manequins.filter(function (m) { return m.checked; });
                            $scope.$watchCollection(function () {
                                return el.selectedManequins;
                            }, common.manageCollectionChange(datacontext.courseSlotManequins, 'id',
                                function (member) {
                                    return {
                                        manequinId: member.id,
                                        courseSlotId: vm.map[indx].id,//slight hack because the collection is replaced by the isteven multiselect - not really sure why this works, but otherwise digest in progress error on change
                                        courseId: vm.course.id
                                    };
                                }));
                        }
                    });
                    vm.log('Activated Course Roles View');
                });
            });
        }

        function changeScenario(m) {
            console.log(m.scenario.briefDescription);
            common.collectionChange(datacontext.courseSlotScenarios, 'id', function (ent) {
                return { 
                    courseSlotId:m.id,
                    scenarioId:ent.id,
                    courseId:vm.course.id
                };
            }, m.scenario?[m.scenario]:[], m.oldScenario?[m.oldScenario]:[]);
            m.oldScenario = m.scenario;
        }

        function calculateCourseRoles(participantId) {
            var returnVar = {
                slotCount: 0,
                scenarioCount: 0
            };
            vm.course.courseSlotPresenters.forEach(function (el) {
                if (el.participantId===participantId) {
                    returnVar.slotCount++;
                }
            });
            vm.course.courseScenarioFacultyRoles.forEach(function (el) {
                if (el.participantId === participantId) {
                    returnVar.scenarioCount++;
                }
            });
            return returnVar;
        }
        //duplicate ctrl key windows/ubuntu, option[alt] key mac, 
        function startSortable(event, ui) {
            if (event.ctrlKey) {
                //ui.placeholder.css('visibility', 'visible').text('[copying]');
                ui.item.sortable.isCopy = true;
            }
        }
        function updateSortable(event, ui) {
            var sortable = ui.item.sortable;
            if (ui.sender) {
                return; 
            } //sender has a value only when the receiving table. in order to cancel both sending and receiving events, cancel must be called from the sending table;
            var participantId = sortable.model.participantId;
            if (sortable.droptargetModel.some(function (el) {
                    return el.participantId === participantId;
            })) { //if the sender, ui.sender will be null - only cancel if the sender
                sortable.cancel();
                return;
            }
            if (ui.item.sortable.isCopy) { //?? should be in stop - add the item back in if in copy mode
                sortable.sourceModel.splice(sortable.index,0,sortable.model);
            }
        }

        function updateSortableRepo(key, repo, event, ui) {
            updateSortable(event, ui);
            var sortable = ui.item.sortable;
            var cancelled = sortable.isCanceled();
            //at the moment this works because the only reason to cancel is if there is a duplicate - beware if cancelling for other reasons
            var removingDuplicate = sortable.droptargetModel.availableFaculty && cancelled;
            if (removingDuplicate) {
                 //? should be in stop
                sortable.sourceModel.splice(sortable.index, 1);
            }

            key.participantId = sortable.model.participantId;
            var ent;
            if (ui.sender && !cancelled) { //receiving = adding 
                ent = repo.getByKey(key);
                if (ent) {
                    if (ent.entityAspect.entityState.isDeleted()) {
                        ent.entityAspect.setUnchanged();
                    } else {
                        log.debug({msg:'attempted to readd existing entity in unknown state', data:ent});
                    }
                } else {
                    repo.create(key);
                }
                
            } else if (!ui.item.sortable.isCopy && (!cancelled || removingDuplicate)) { //sending = deleting
                ent = repo.getByKey(key);
                ent.entityAspect.setDeleted();
            }

            angular.extend(sortable.model, calculateCourseRoles(key.participantId));
        }
    }
})();
