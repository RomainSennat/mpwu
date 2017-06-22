package com.example.demo;

import org.springframework.data.jpa.repository.JpaRepository;

/**
 * Created by romain on 22/06/2017.
 */
public interface AlarmRepository extends JpaRepository<AlarmEntity, Integer> {


}
